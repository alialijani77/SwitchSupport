﻿using Microsoft.EntityFrameworkCore;
using SwitchSupport.DataLayer.Context;
using SwitchSupport.Domain.Entities.Account;
using SwitchSupport.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.DataLayer.Repositories.Account
{
    public class UserRepository : IUserRepository
    {
        private readonly SwitchSupportDbContext _context;

        #region ctor
        public UserRepository(SwitchSupportDbContext context)
        {
            _context = context;
        }
        #endregion


        public async Task<bool> IsExistsUserByEmail(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task CreateUser(User user)
        {
           await _context.Users.AddAsync(user);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByActivationcode(string activationcode)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.EmailActivationCode.Equals(activationcode));
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public async Task<User?> GetUserById(long userId)
        {
            return await _context.Users.SingleOrDefaultAsync(u => !u.IsDelete && u.Id == userId);
        }

        public IQueryable<User> GetUserIQueryable()
        {
            return _context.Users.Where(u => !u.IsDelete).AsQueryable();
        }

        #region Permisson

        public async Task<bool> CheckUserHasPermission(long permissionId, long userId)
        {
            return await _context.UserPermissions.AnyAsync(u => u.PermissionId == permissionId && u.UserId == userId);
        }
        #endregion
    }
}

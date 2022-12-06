using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Domain.ViewModels.Common
{
    public class Paging<T>
    {
        public Paging()
        {
            CurrentPage = 1;
            HowManyShowBeforAfter = 3;
            TakeEntitiy = 3;
            Entities = new List<T>();
        }
        public int CurrentPage { get; set; }

        public int StartPage { get; set; }

        public int EndPage { get; set; }

        public int TotalPage { get; set; }

        public int HowManyShowBeforAfter { get; set; }

        public int TakeEntitiy { get; set; }

        public int SkipEntity { get; set; }

        public int AllCountEntities { get; set; }

        public List<T> Entities { get; set; }


        public PagingViewModel GetPaging()
        {
            var res = new PagingViewModel();
            res.CurrentPage = this.CurrentPage;
            res.StartPage = this.StartPage;
            res.EndPage = this.EndPage;
            return res;
        }

        public async Task SetPaging(IQueryable<T> query)
        {
            AllCountEntities = query.Count();
            TotalPage = (int)Math.Ceiling(AllCountEntities / (double)TakeEntitiy);
            CurrentPage = CurrentPage < 1 ? 1 : CurrentPage;
            CurrentPage = CurrentPage > TotalPage ? TotalPage : CurrentPage;
            StartPage = CurrentPage - HowManyShowBeforAfter < 0 ? 1 : CurrentPage - HowManyShowBeforAfter;
            EndPage = CurrentPage + HowManyShowBeforAfter > TotalPage ? TotalPage : CurrentPage + HowManyShowBeforAfter;
            SkipEntity = (2 - 1) * TakeEntitiy;
            Entities = query.Skip(SkipEntity).Take(TakeEntitiy).ToList();
        }
    }

    public class PagingViewModel
    {
        public int CurrentPage { get; set; }

        public int StartPage { get; set; }

        public int EndPage { get; set; }
    }
}

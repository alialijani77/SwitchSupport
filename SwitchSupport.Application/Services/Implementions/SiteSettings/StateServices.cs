using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.Interfaces;
using SwitchSupport.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Application.Services.Implementions.SiteSettings
{
    public class StateServices : IStateServices
    {
        private readonly IStateRepository _stateRepository;

        public StateServices(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }
        public async Task<List<SelectListViewModel>> GetAllState(long? stateId = null)
        {
            var res = await _stateRepository.GetAllState(stateId);

            return res.Select(s => new SelectListViewModel(){ Id = s.Id, Title = s.Title }).ToList();                   
            
        }
    }
}

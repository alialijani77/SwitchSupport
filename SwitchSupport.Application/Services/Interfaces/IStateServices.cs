using SwitchSupport.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Application.Services.Interfaces
{
    public interface IStateServices
    {
        Task<List<SelectListViewModel>> GetAllState(long? stateId = null);
    }
}

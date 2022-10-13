using SwitchSupport.Domain.Entities.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Domain.Interfaces
{
    public interface IStateRepository
    {
        Task<List<State>> GetAllState(long? stateId = null);
    }
}

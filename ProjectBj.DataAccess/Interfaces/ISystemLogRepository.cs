using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface ISystemLogRepository
    {
        Task<IEnumerable<SystemLog>> GetAllLogs();
    }
}

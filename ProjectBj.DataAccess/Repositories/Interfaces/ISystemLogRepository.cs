using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories.Interfaces
{
    public interface ISystemLogRepository
    {
        Task<IEnumerable<SystemLog>> GetAll();
    }
}

using ProjectBj.DataAccess.Repositories.Interfaces;
using ProjectBj.Entities;

namespace ProjectBj.DataAccess.Repositories
{
    public class SystemLogRepository : BaseRepository<SystemLog>, ISystemLogRepository
    {
        public SystemLogRepository(string connectionString): base(connectionString)
        {
        }
    }
}

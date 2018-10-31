using ProjectBj.DataAccess.Repositories.Interfaces;
using ProjectBj.Entities;

namespace ProjectBj.DataAccess.Repositories
{
    public class HistoryRepository : BaseRepository<History>, IHistoryRepository
    {
        public HistoryRepository(string connectionString): base(connectionString)
        {
        }
    }
}

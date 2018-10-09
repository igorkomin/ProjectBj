using Dapper;
using ProjectBj.DataAccess.Repositories.Interfaces;
using ProjectBj.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories
{
    public class HistoryRepository : BaseRepository<History>, IHistoryRepository
    {
        private readonly string _connectionString;

        public HistoryRepository(string connectionString): base(connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<History>> GetBySessionId(long sessionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"SELECT * FROM History WHERE SessionId = @sessionId";
                IEnumerable<History> history = await db.QueryAsync<History>(sqlQuery, new { sessionId });
                return history;
            }
        }
    }
}

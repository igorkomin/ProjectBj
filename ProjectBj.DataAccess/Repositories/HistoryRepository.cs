using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly string _connectionString;

        public HistoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Create(History entry)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.InsertAsync(entry);
            }
        }

        public async Task<IEnumerable<History>> GetBySessionId(long sessionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"SELECT * FROM History WHERE SessionId = @sessionId";
                IEnumerable<History> logs = await db.QueryAsync<History>(sqlQuery, new { sessionId });
                return logs;
            }
        }

        public async Task<IEnumerable<History>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<History> logs = await db.GetAllAsync<History>();
                return logs;
            }
        }
    }
}

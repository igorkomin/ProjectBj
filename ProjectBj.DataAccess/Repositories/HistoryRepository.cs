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

        public async Task<ICollection<History>> GetBySessionId(int sessionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"SELECT * FROM Logs WHERE SessionId = @sessionId";
                var logs = await db.QueryAsync<History>(sqlQuery, new { sessionId });
                return logs.AsList();
            }
        }

        public async Task<ICollection<History>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var logs = await db.GetAllAsync<History>();
                return logs.AsList();
            }
        }
    }
}

using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.DataAccess.ExceptionHandlers;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Logger;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories
{
    public class GameLogRepository : IGameLogRepository
    {
        private readonly string _connectionString;

        public GameLogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task CreateEntry(LogEntry entry)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    await db.InsertAsync(entry);
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<ICollection<LogEntry>> GetLogsBySessionId(int sessionId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sqlQuery = @"SELECT * FROM Logs WHERE SessionId = @sessionId";
                    var logs = await db.QueryAsync<LogEntry>(sqlQuery, new { sessionId });
                    return logs.AsList();
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<ICollection<LogEntry>> GetAllLogs()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var logs = await db.GetAllAsync<LogEntry>();
                    return logs.AsList();
                }
            }
            catch(SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }
    }
}

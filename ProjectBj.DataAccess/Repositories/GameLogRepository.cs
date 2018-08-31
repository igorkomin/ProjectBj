using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.Entities;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.DataAccess.ExceptionHandlers;
using ProjectBj.Logger;

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

        public async Task<ICollection<LogEntry>> GetSessionLogs(int sessionId)
        {
            var allLogs = await GetAllLogs();
            var sessionLogs = allLogs.Where(x => x.SessionId == sessionId);
            return sessionLogs.AsList();
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

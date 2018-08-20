using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
using ProjectBj.DataAccess.ExceptionHandlers;

namespace ProjectBj.DataAccess.Repositories
{
    public class LogRepository : ILogRepository
    {
        public async Task CreateEntry(LogEntry entry)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    await db.InsertAsync(entry);
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<LogEntry> GetEntry(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var entry = await db.GetAsync<LogEntry>(id);
                    return entry;
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<ICollection<LogEntry>> GetAllLogs()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var logs = await db.GetAllAsync<LogEntry>();
                    return logs.AsList();
                }
            }
            catch(SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task DeleteEntry(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "DELETE FROM Logs WHERE Id = @id";
                    await db.ExecuteAsync(sqlQuery, new { id });
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }
    }
}

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
        private string _connectionString;

        public LogRepository(string connectionString)
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
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<LogEntry> GetEntryById(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
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
                using (IDbConnection db = new SqlConnection(_connectionString))
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

        public async Task DeleteEntry(LogEntry entry)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    await db.DeleteAsync(entry);
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }
    }
}

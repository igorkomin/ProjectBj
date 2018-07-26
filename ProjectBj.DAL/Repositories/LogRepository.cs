using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using ProjectBj.DAL.Interfaces;
using ProjectBj.Entities;
using ProjectBj.DAL.ExceptionHandlers;

namespace ProjectBj.DAL.Repositories
{
    public class LogRepository : ILogRepository
    {
        public async Task CreateEntry(LogEntry entry)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "INSERT INTO Logs (SessionId, Time, Message) VALUES (@SessionId, @Time, @Message)";
                    await db.ExecuteAsync(sqlQuery, entry);
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
                    var sqlQuery = "SELECT * FROM Logs WHERE Id = @id";
                    var entry = await db.QueryAsync<LogEntry>(sqlQuery, new { id });
                    return entry.FirstOrDefault();
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public ICollection<LogEntry> GetAllLogs()
        {
            List<LogEntry> logs = new List<LogEntry>();
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "SELECT * FROM Logs";
                    logs = db.Query<LogEntry>(sqlQuery).AsList();
                }
                return logs;
            }
            catch(SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public void DeleteEntry(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "DELETE FROM Logs WHERE Id = @id";
                    db.Execute(sqlQuery, new { id });
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }
    }
}

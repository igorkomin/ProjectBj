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
        public void CreateEntry(LogEntry entry)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "INSERT INTO Logs (SessionId, Time, Message) VALUES (@SessionId, @Time, @Message)";
                    db.Execute(sqlQuery, entry);
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public LogEntry GetEntry(int id)
        {
            LogEntry entry;
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "SELECT * FROM Logs WHERE Id = @id";
                    entry = db.Query<LogEntry>(sqlQuery, new { id }).FirstOrDefault();
                }
                return entry;
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

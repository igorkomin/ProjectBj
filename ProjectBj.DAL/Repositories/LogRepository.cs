using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using ProjectBj.Common.ExceptionHandlers;
using ProjectBj.Configuration;
using ProjectBj.Entities;

namespace ProjectBj.DAL.Repositories
{
    public class LogRepository
    {
        public void CreateEntry(LogEntry entry)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
                {
                    var sqlQuery = "INSERT INTO Logs (Time, Message) VALUES (@Time, @Message)";
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
            LogEntry entry = new LogEntry();
            try
            {
                using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
                {
                    var sqlQuery = "SELECT * FROM Logs WHERE Id = @id";
                    entry = db.Query<LogEntry>(sqlQuery, new { id }).FirstOrDefault();
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
            return entry;
        }

        public ICollection<LogEntry> GetAllLogs()
        {
            List<LogEntry> logs = new List<LogEntry>();
            try
            {
                using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
                {
                    var sqlQuery = "SELECT * FROM Logs";
                    logs = db.Query<LogEntry>(sqlQuery).ToList();
                }
            }
            catch(SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
            return logs;
        }

        public void DeleteEntry(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
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

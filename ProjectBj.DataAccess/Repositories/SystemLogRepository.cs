using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.DataAccess.ExceptionHandlers;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Logger;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories
{
    public class SystemLogRepository : ISystemLogRepository
    {
        private readonly string _connectionString;

        public SystemLogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<SystemLog>> GetAllLogs()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var systemLogs = await db.GetAllAsync<SystemLog>();
                    return systemLogs.AsList();
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }
    }
}

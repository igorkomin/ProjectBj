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
using ProjectBj.DataAccess.ExceptionHandlers;
using ProjectBj.Entities;
using ProjectBj.Logger;

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

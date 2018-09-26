using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.DataAccess.Repositories.Interfaces;
using ProjectBj.Entities;
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

        public async Task<IEnumerable<SystemLog>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<SystemLog> systemLogs = await db.GetAllAsync<SystemLog>();
                return systemLogs;
            }
        }
    }
}

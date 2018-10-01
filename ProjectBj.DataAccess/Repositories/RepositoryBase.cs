using Dapper.Contrib.Extensions;
using ProjectBj.DataAccess.Repositories.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories
{
    public abstract class RepositoryBase<T>: IRepositoryBase<T> where T: class
    {
        private readonly string _connectionString;

        public RepositoryBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<T> Insert(T item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.InsertAsync(item);
                return item;
            }
        }

        public async Task<T> GetById(long id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                T item = await db.GetAsync<T>(id);
                return item;
            }
        }

        public async Task Update(T item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.UpdateAsync(item);
            }
        }

        public async Task Delete(T item)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.DeleteAsync(item);
            }
        }
    }
}

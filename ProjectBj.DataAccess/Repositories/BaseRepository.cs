﻿using Dapper.Contrib.Extensions;
using ProjectBj.DataAccess.Repositories.Interfaces;
using ProjectBj.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories
{
    public abstract class BaseRepository<T>: IBaseRepository<T> where T: BaseEntity
    {
        private readonly string _connectionString;

        protected BaseRepository(string connectionString)
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

        public async Task Insert(IEnumerable<T> items)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.InsertAsync(items);
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

        public async Task<IEnumerable<T>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<T> items = await db.GetAllAsync<T>();
                return items;
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

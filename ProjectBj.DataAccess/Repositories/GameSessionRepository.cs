﻿using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories
{
    public class GameSessionRepository : IGameSessionRepository
    {
        private readonly string _connectionString;

        public GameSessionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<GameSession> Insert(GameSession session)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                session.Id = await db.InsertAsync(session);
                return session;
            }
        }

        public async Task<GameSession> GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var session = await db.GetAsync<GameSession>(id);
                return session;
            }
        }

        public async Task Update(GameSession session)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.UpdateAsync(session);
            }
        }

        public async Task<GameSession> GetFirstOpenSession(int playerId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"SELECT DISTINCT gs.* FROM PlayerHands ph
                                     JOIN GameSessions gs ON ( ph.SessionId = gs.Id )
                                     WHERE gs.IsOpen = 1 AND ph.PlayerId = @playerId";

                var session = await db.QueryAsync<GameSession>(sqlQuery, new { playerId });
                return session.FirstOrDefault();
            }
        }
    }
}

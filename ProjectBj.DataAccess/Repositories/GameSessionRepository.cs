﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.Entities;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.DataAccess.ExceptionHandlers;
using ProjectBj.Logger;

namespace ProjectBj.DataAccess.Repositories
{
    public class GameSessionRepository : IGameSessionRepository
    {
        private string _connectionString;

        public GameSessionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<GameSession> Create(GameSession session)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    session.Id = await db.InsertAsync(session);
                    return session;
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<GameSession> GetById(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var session = await db.GetAsync<GameSession>(id);
                    return session;
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task Update(GameSession session)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    await db.UpdateAsync(session);
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task Delete(GameSession session)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    await db.DeleteAsync(session);
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<ICollection<Player>> GetSessionPlayers(GameSession session)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sqlQuery = "SELECT p.* FROM GameSessionPlayers gsp " +
                                   "JOIN Players p ON ( gsp.PlayerId = p.Id ) " +
                                   "JOIN GameSessions gs ON ( gsp.SessionId = gs.Id ) " +
                                   "WHERE gs.Id = @Id";
                    var players = await db.QueryAsync<Player>(sqlQuery, session);
                    return players.AsList();
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<ICollection<GameSession>> GetPlayerSessions(Player player)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sqlQuery = "SELECT gs.* FROM PlayerHands ph " +
                                   "JOIN Players p ON ( ph.PlayerId = p.Id ) " +
                                   "JOIN GameSessions gs ON ( ph.PlayerId = gs.Id ) " +
                                   "WHERE p.Id = @Id";

                    var sessions = await db.QueryAsync<GameSession>(sqlQuery, player);
                    return sessions.AsList();
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<GameSession> GetCurrentSession(Player player)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sqlQuery = "SELECT gs.* FROM PlayerHands ph " +
                                   "JOIN Players p ON ( ph.PlayerId = p.Id ) " +
                                   "JOIN GameSessions gs ON ( ph.SessionId = gs.Id ) " +
                                   "WHERE gs.IsOpen = 1 AND p.Id = @Id";

                    var session = await db.QueryAsync<GameSession>(sqlQuery, player);
                    return session.FirstOrDefault();
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

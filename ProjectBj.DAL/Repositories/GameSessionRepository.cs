﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ProjectBj.Entities;
using ProjectBj.DAL.Interfaces;
using ProjectBj.DAL.ExceptionHandlers;

namespace ProjectBj.DAL.Repositories
{
    public class GameSessionRepository : IGameSessionRepository
    {
        public async Task<GameSession> Create(GameSession session)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "INSERT INTO GameSessions (TimeCreated) " +
                                   "VALUES (@TimeCreated);";
                    await db.ExecuteAsync(sqlQuery, session);
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
            return session;
        }

        public async Task Update(GameSession session)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "UPDATE GameSessions " +
                                   "SET IsOpen = @IsOpen " +
                                   "WHERE Id = @Id";
                    await db.ExecuteAsync(sqlQuery, session);
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task Delete(GameSession session)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "DELETE FROM GameSessions " +
                                   "WHERE Id = @Id";
                    await db.ExecuteAsync(sqlQuery, session);
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task AddPlayer(GameSession session, Player player)
        {
            try
            {
                GameSessionPlayer gameSessionPlayer = new GameSessionPlayer() { SessionId = session.Id, PlayerId = player.Id };
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "INSERT INTO GameSessionPlayers (SessionId, PlayerId, TimeJoined) " +
                                   "VALUES (@SessionId, @PlayerId, @TimeJoined)";
                    await db.ExecuteAsync(sqlQuery, gameSessionPlayer);
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<ICollection<Player>> GetSessionPlayers(GameSession session)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
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
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public ICollection<GameSession> GetPlayerSessions(Player player)
        {
            List<GameSession> sessions;
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "SELECT gs.* FROM GameSessionPlayers gsp " +
                                   "JOIN Players p ON ( gsp.PlayerId = p.Id ) " +
                                   "JOIN GameSessions gs ON ( gsp.PlayerId = gs.Id ) " +
                                   "WHERE p.Id = @Id";

                    sessions = db.Query<GameSession>(sqlQuery, player).AsList();
                    return sessions;
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public GameSession GetCurrentSession(Player player)
        {
            GameSession session;

            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "SELECT gs.* FROM GameSessionPlayers gsp " +
                                   "JOIN Players p ON ( gsp.PlayerId = p.Id ) " +
                                   "JOIN GameSessions gs ON ( gsp.PlayerId = gs.Id ) " +
                                   "WHERE gs.IsOpen = 1 AND p.Id = @Id";

                    session = db.Query<GameSession>(sqlQuery, player).FirstOrDefault();
                    return session;
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }
    }
}

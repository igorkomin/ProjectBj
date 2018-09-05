using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.DataAccess.ExceptionHandlers;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly string _connectionString;

        public PlayerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Player> Create(Player player)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    await db.InsertAsync(player);
                    return player;
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<ICollection<Player>> Create(ICollection<Player> players)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    await db.InsertAsync(players);
                    return players;
                }
            }
            catch(SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<ICollection<Player>> FindPlayers(string name)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var players = (await db.GetAllAsync<Player>()).AsList().FindAll(x => x.Name == name);
                    return players;
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<Player> GetById(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var player = await db.GetAsync<Player>(id);
                    return player;
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task Update(Player player)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    await db.UpdateAsync(player);
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }

        }

        public async Task AddCard(Player player, int cardId, int sessionId)
        {
            try
            {
                PlayerHand playerHand = new PlayerHand
                {
                    PlayerId = player.Id,
                    CardId = cardId,
                    SessionId = sessionId
                };
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    await db.InsertAsync(playerHand);
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<ICollection<Player>> GetSessionBots(int sessionId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sqlQuery = @"SELECT DISTINCT p.* FROM PlayerHands ph
                                     JOIN Players p ON ( ph.PlayerId = p.Id )
                                     WHERE ph.SessionId = @sessionId
                                     AND p.IsHuman = 0 AND p.InGame = 1";
                    var bots = await db.QueryAsync<Player>(sqlQuery, new { sessionId });
                    return bots.AsList();
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task DeleteNonHumanPlayers(int sessionId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sessionBots = await GetSessionBots(sessionId);
                    await db.DeleteAsync(sessionBots);
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }
    }
}

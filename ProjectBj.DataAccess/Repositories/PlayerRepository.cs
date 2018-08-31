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

        public async Task<Player> CreateOne(Player player)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    player.Id = await db.InsertAsync(player);
                    return player;
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<ICollection<Player>> CreateMany(ICollection<Player> players)
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
                    var sqlQuery = "SELECT * FROM Players WHERE Name = @name";
                    var players = await db.QueryAsync<Player>(sqlQuery, new { name });
                    return players.AsList();
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

        public async Task<ICollection<Card>> GetCards(Player player, int sessionId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sqlQuery = "SELECT c.* FROM PlayerHands ph " +
                                   "JOIN Cards c ON ( ph.CardId = c.Id ) " +
                                   //"JOIN Players p ON ( ph.PlayerId = p.Id ) " +
                                   "WHERE ph.PlayerId = @Id " +
                                   $"AND ph.SessionId = {sessionId}";
                    var cards = await db.QueryAsync<Card>(sqlQuery, player);
                    return cards.AsList();
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
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT DISTINCT p.* FROM PlayerHands ph " +
                               "JOIN Players p ON ( ph.PlayerId = p.Id ) " +
                               "WHERE ph.SessionId = @sessionId " +
                               "AND p.IsHuman = 0 " +
                               "AND p.InGame = 1";
                var bots = await db.QueryAsync<Player>(sqlQuery, new { sessionId });
                return bots.AsList();
            }
        }

        public async Task DeleteNonHumanPlayers(string dealerName)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sqlQuery = $"DELETE FROM Players WHERE IsHuman = 0 AND Name <> '{dealerName}'";
                    await db.ExecuteAsync(sqlQuery);
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

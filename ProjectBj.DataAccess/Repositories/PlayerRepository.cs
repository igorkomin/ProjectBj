using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Logger;
using ProjectBj.DataAccess.ExceptionHandlers;

namespace ProjectBj.DataAccess.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        public async Task<Player> CreateOne(Player player)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    player.Id = await db.InsertAsync(player);
                    return player;
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<ICollection<Player>> CreateMany(ICollection<Player> players)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    foreach (var player in players)
                    {
                        player.Id = await db.InsertAsync(player);
                    }
                    return players;
                }
            }
            catch(SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task Delete(Player player)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    await db.DeleteAsync(player);
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<ICollection<Player>> FindPlayers(string name)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "SELECT * FROM Players WHERE Name = @name";
                    var players = await db.QueryAsync<Player>(sqlQuery, new { name });
                    return players.AsList();
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<Player> Get(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var player = await db.GetAsync<Player>(id);
                    return player;
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<ICollection<Player>> GetAllPlayers()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var players = await db.GetAllAsync<Player>();
                    return players.AsList();
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task Update(Player player)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    await db.UpdateAsync(player);
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }

        }

        public async Task AddCard(Player player, Card card, int sessionId)
        {
            try
            {
                PlayerHand playerHand = new PlayerHand
                {
                    PlayerId = player.Id,
                    CardId = card.Id,
                    SessionId = sessionId
                };
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    await db.InsertAsync(playerHand);
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<ICollection<Card>> GetCards(Player player, int sessionId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "SELECT c.* FROM PlayerHands ph " +
                                   "JOIN Cards c ON ( ph.CardId = c.Id ) " +
                                   "JOIN Players p ON ( ph.PlayerId = p.Id ) " +
                                   "WHERE ph.PlayerId = @Id " +
                                   $"AND ph.SessionId = {sessionId}";
                    var cards = await db.QueryAsync<Card>(sqlQuery, player);
                    return cards.AsList();
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<ICollection<Player>> GetSessionBots(int sessionId)
        {
            using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
            {
                var sqlQuery = "SELECT p.* FROM PlayerHands ph " +
                               "JOIN Players p ON ( ph.PlayerId = p.Id ) " +
                               "WHERE ph.SessionId = @sessionId " +
                               "AND p.IsHuman = 0 " +
                               "AND p.InGame = 1";
                var bots = await db.QueryAsync<Player>(sqlQuery, new { sessionId });
                return bots.AsList();
            }
        }

        public async Task DeleteCards(Player player)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "DELETE FROM PlayerHands WHERE PlayerId = @Id";
                    await db.ExecuteAsync(sqlQuery, player);
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task DeletePlayersByName(string name)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "DELETE FROM Players WHERE Name = @name";
                    await db.ExecuteAsync(sqlQuery, new { name });
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }
    }
}

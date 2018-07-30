using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using Dapper;
using ProjectBj.DAL.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Logger;
using ProjectBj.DAL.ExceptionHandlers;

namespace ProjectBj.DAL.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly string _insertQuery = "INSERT INTO Players (Name, Balance, IsHuman, InGame) " +
                                               "VALUES(@Name, @Balance, @IsHuman, @InGame); " +
                                               "SELECT CAST(SCOPE_IDENTITY() as int)";

        public async Task<Player> CreateOne(Player player)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var playerId = await db.QueryAsync<int>(_insertQuery, player);
                    player.Id = playerId.FirstOrDefault();
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
                        var playerId = await db.QueryAsync<int>(_insertQuery, player);
                        player.Id = playerId.FirstOrDefault();
                    }
                    return players;
                }
            }
            catch(SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "DELETE FROM Players WHERE Id = @id";
                    await db.ExecuteAsync(sqlQuery, new { id });
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
                    var sqlQuery = "SELECT * FROM Players WHERE Id = @id";
                    var player = await db.QueryAsync<Player>(sqlQuery, new { id });
                    return player.FirstOrDefault();
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
                    var sqlQuery = "SELECT * FROM Players";
                    var players = await db.QueryAsync<Player>(sqlQuery);
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
                    var sqlQuery = "UPDATE Players " +
                                   "SET Name = @Name, IsHuman = @IsHuman, Balance = @Balance, InGame = @Ingame " +
                                   "WHERE Id = @Id";
                    await db.ExecuteAsync(sqlQuery, player);
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
                PlayerHand playerHand = new PlayerHand() { PlayerId = player.Id, CardId = card.Id, SessionId = sessionId };
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "INSERT INTO PlayerHands (PlayerId, CardId, SessionId) " +
                                   "VALUES (@PlayerId, @CardId, @SessionId)";
                    await db.ExecuteAsync(sqlQuery, playerHand);
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
                               "AND p.IsHuman = false " +
                               "AND p.InGame = true";
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
                throw new DataSourceException(exception.Message, exception); ;
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

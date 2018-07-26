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
        private string _insertQuery = "INSERT INTO Players (Name, Balance, IsHuman, InGame) " +
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

        public Player Get(int id)
        {
            Player player;
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "SELECT * FROM Players WHERE Id = @id";
                    player = db.Query<Player>(sqlQuery, new { id }).FirstOrDefault();
                }
                return player;
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public ICollection<Player> GetAllPlayers()
        {
            List<Player> players;
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "SELECT * FROM Players";
                    players = db.Query<Player>(sqlQuery).AsList();
                }
                return players;
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public void Update(Player player)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "UPDATE Players " +
                                   "SET Name = @Name, IsHuman = @IsHuman, Balance = @Balance, InGame = @Ingame " +
                                   "WHERE Id = @Id";
                    db.Execute(sqlQuery, player);
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }

        }

        public void AddCard(Player player, Card card)
        {
            try
            {
                PlayerHand playerHand = new PlayerHand() { PlayerId = player.Id, CardId = card.Id };
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "INSERT INTO PlayerHands (PlayerId, CardId) " +
                                   "VALUES (@PlayerId, @CardId)";
                    db.Execute(sqlQuery, playerHand);
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public ICollection<Card> GetCards(Player player)
        {
            List<Card> cards;
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "SELECT c.* FROM PlayerHands ph " +
                                   "JOIN Cards c ON ( ph.CardId = c.Id ) " +
                                   "JOIN Players p ON ( ph.PlayerId = p.Id ) " +
                                   "WHERE ph.PlayerId = @Id";
                    cards = db.Query<Card>(sqlQuery, player).AsList();
                }
                return cards;
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public void DeleteCards(Player player)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "DELETE FROM PlayerHands WHERE PlayerId = @Id";
                    db.Execute(sqlQuery, player);
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception); ;
            }
        }

        public void DeletePlayersByName(string name)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "DELETE FROM Players WHERE Name = @name";
                    db.Execute(sqlQuery, new { name });
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }
    }
}

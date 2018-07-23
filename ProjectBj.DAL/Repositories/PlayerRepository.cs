using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using Dapper;
using ProjectBj.Common.ExceptionHandlers;
using ProjectBj.Configuration;
using ProjectBj.Entities;
using ProjectBj.Logger;

namespace ProjectBj.DAL.Repositories
{
    public class PlayerRepository
    {
        private string _insertQuery = "INSERT INTO Players (Name, Balance, IsHuman, InGame) " +
                                      "VALUES(@Name, @Balance, @IsHuman, @InGame); " +
                                      "SELECT CAST(SCOPE_IDENTITY() as int)";

        public Player CreateOne(Player player)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
                {
                    int playerId = db.Query<int>(_insertQuery, player).FirstOrDefault();
                    player.Id = playerId;
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
            return player;
        }

        public ICollection<Player> CreateMany(ICollection<Player> players)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
                {
                    foreach (var player in players)
                    {
                        int playerId = db.Query<int>(_insertQuery, player).FirstOrDefault();
                        player.Id = playerId;
                    }
                }
            }
            catch(SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
            return players;
        }

        public void Delete(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
                {
                    var sqlQuery = "DELETE FROM Players WHERE Id = @id";
                    db.Execute(sqlQuery, new { id });
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public ICollection<Player> FindPlayers(string name)
        {
            List<Player> players;
            try
            {
                using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
                {
                    var sqlQuery = "SELECT * FROM Players WHERE Name = @name";
                    players = db.Query<Player>(sqlQuery, new { name }).ToList();
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
            return players;
        }

        public Player Get(int id)
        {
            Player player;
            try
            {
                using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
                {
                    var sqlQuery = "SELECT * FROM Players WHERE Id = @id";
                    player = db.Query<Player>(sqlQuery, new { id }).FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
            return player;
        }

        public ICollection<Player> GetAllPlayers()
        {
            List<Player> players;
            try
            {
                using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
                {
                    var sqlQuery = "SELECT * FROM Players";
                    players = db.Query<Player>(sqlQuery).ToList();
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
            return players;
        }

        public void Update(Player player)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
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
                using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
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
                using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
                {
                    var sqlQuery = "SELECT c.* FROM playerhands ph " +
                                   "JOIN Cards c ON ( ph.CardId = c.Id ) " +
                                   "JOIN Players p ON ( ph.PlayerId = p.Id ) " +
                                   "WHERE ph.PlayerId = @Id";
                    cards = db.Query<Card>(sqlQuery, player).ToList();
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
            return cards;
        }

        public void DeletePlayersByName(string name)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
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

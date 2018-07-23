using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using Dapper;
using ProjectBj.Entities;
using ProjectBj.Configuration;
using ProjectBj.Logger;

namespace ProjectBj.DAL.Repositories
{
    public class PlayerRepository
    {
        public Player Create(Player player)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
                {
                    var sqlQuery = "INSERT INTO Players (Name, Balance, IsHuman, InGame) " +
                                   "VALUES(@Name, @Balance, @IsHuman, @InGame); " +
                                   "SELECT CAST(SCOPE_IDENTITY() as int)";
                    int playerId = db.Query<int>(sqlQuery, player).FirstOrDefault();
                    player.Id = playerId;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return player;
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
            catch (Exception exception)
            {
                throw exception;
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
            catch (Exception exception)
            {
                throw exception;
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
                throw exception;
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
            catch (Exception exception)
            {
                throw exception;
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
            catch (Exception exception)
            {
                throw exception;
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
            catch (Exception exception)
            {
                throw exception;
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
            catch (Exception exception)
            {
                throw exception;
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
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

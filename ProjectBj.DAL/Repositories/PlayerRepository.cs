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
        IDbConnection _db;

        public Player Create(Player player)
        {
            try
            {
                _db = new SqlConnection(AppStrings.ConnectionString);
                var sqlQuery = "INSERT INTO Players (Name, Balance, IsHuman, InGame) VALUES(@Name, @Balance, @IsHuman, @InGame); SELECT CAST(SCOPE_IDENTITY() as int)";
                int playerId = _db.Query<int>(sqlQuery, player).FirstOrDefault();
                player.Id = playerId;
            }
            catch 
            {
                throw;
            }
            finally
            {
                _db.Close();
            }

            return player;
        }

        public void Delete(int id)
        {
            try
            {
                _db = new SqlConnection(AppStrings.ConnectionString);
                var sqlQuery = "DELETE FROM Players WHERE Id = @id";
                _db.Execute(sqlQuery, new { id });
            }
            catch
            {
                throw;
            }
            finally
            {
                _db.Close();
            }
        }

        public ICollection<Player> FindPlayers(string name)
        {
            List<Player> players;
            try
            {
                _db = new SqlConnection(AppStrings.ConnectionString);
                var sqlQuery = "SELECT * FROM Players WHERE Name = @name";
                players = _db.Query<Player>(sqlQuery, new { name }).ToList();
            }
            catch
            {
                throw;
            }
            finally
            {
                _db.Close();
            }
            return players;
        }

        public Player Get(int id)
        {
            Player player;
            try
            {
                _db = new SqlConnection(AppStrings.ConnectionString);
                var sqlQuery = "SELECT * FROM Players WHERE Id = @id";
                player = _db.Query<Player>(sqlQuery, new { id }).FirstOrDefault();
            }
            catch
            {
                throw;
            }
            return player;
        }

        public ICollection<Player> GetAllPlayers()
        {
            List<Player> players;
            try
            {
                _db = new SqlConnection(AppStrings.ConnectionString);
                var sqlQuery = "SELECT * FROM Players";
                players = _db.Query<Player>(sqlQuery).ToList();
            }
            catch
            {
                throw;
            }
            return players;
        }

        public void Update(Player player)
        {
            try
            {
                _db = new SqlConnection(AppStrings.ConnectionString);
                var sqlQuery = "UPDATE Players SET Name = @Name, IsHuman = @IsHuman, Balance = @Balance, InGame = @Ingame WHERE Id = @Id";
                _db.Execute(sqlQuery, player);
            }
            catch
            {
                throw;
            }
        }

        public void AddCard(Player player, Card card)
        {
            PlayerHand playerHand = new PlayerHand() { PlayerId = player.Id, CardId = card.Id };
            try
            {
                _db = new SqlConnection(AppStrings.ConnectionString);
                var sqlQuery = "INSERT INTO PlayerHands (PlayerId, CardId) VALUES(@PlayerId, @CardId)";
                _db.Execute(sqlQuery, playerHand);
            }
            catch
            {
                throw;
            }
        }

        public ICollection<Card> GetCards(Player player)
        {
            List<Card> cards;
            try
            {
                _db = new SqlConnection(AppStrings.ConnectionString);
                var sqlQuery = "SELECT c.* FROM playerhands ph JOIN Cards c ON ( ph.CardId = c.Id ) JOIN Players p ON ( ph.PlayerId = p.Id ) WHERE ph.PlayerId = @Id";
                cards = _db.Query<Card>(sqlQuery, player).ToList();
            }
            catch
            {
                throw;
            }
            return cards;
        }
    }
}

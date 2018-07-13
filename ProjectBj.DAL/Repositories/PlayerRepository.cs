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

namespace ProjectBj.DAL.Repositories
{
    public class PlayerRepository
    { 
        public Player Create(Player player)
        {
            using (IDbConnection db = new SqlConnection(AppStrings.connectionString))
            {
                var sqlQuery = "INSERT INTO Players (Name, Balance, IsHuman, InGame) VALUES(@Name, @Balance, @IsHuman, @InGame); SELECT CAST(SCOPE_IDENTITY() as int)";
                int userId = db.Query<int>(sqlQuery, player).FirstOrDefault();
                player.Id = userId;
            }
            return player;
        }

        public void Delete(int id)
        {
            using(IDbConnection db = new SqlConnection(AppStrings.connectionString))
            {
                var sqlQuery = "DELETE FROM Players WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public ICollection<Player> FindPlayers(string name)
        {
            List<Player> players;
            using (IDbConnection db = new SqlConnection(AppStrings.connectionString))
            {
                var sqlQuery = "SELECT * FROM Players WHERE Name = @name";
                players = db.Query<Player>(sqlQuery, new { name }).ToList();
            }
            return players;
        }

        public Player Get(int id)
        {
            Player player;
            using (IDbConnection db = new SqlConnection(AppStrings.connectionString))
            {
                var sqlQuery = "SELECT * FROM Players WHERE Id = @id";
                player = db.Query<Player>(sqlQuery, new { id }).FirstOrDefault();
            }
            return player;
        }

        public Player Get(Player player)
        {
            using (IDbConnection db = new SqlConnection(AppStrings.connectionString))
            {
                var sqlQuery = "SELECT * FROM Players WHERE Id = @Id";
                player = db.Query<Player>(sqlQuery, player).FirstOrDefault();
            }
            return player;
        }

        public ICollection<Player> GetAllPlayers()
        {
            List<Player> players;
            using (IDbConnection db = new SqlConnection(AppStrings.connectionString))
            {
                var sqlQuery = "SELECT * FROM Players";
                players = db.Query<Player>(sqlQuery).ToList();
            }
            return players;
        }

        public void Update(Player player)
        {
            using (IDbConnection db = new SqlConnection(AppStrings.connectionString))
            {
                var sqlQuery = "UPDATE Players SET Name = @Name, IsHuman = @IsHuman, Balance = @Balance, InGame = @Ingame";
                db.Execute(sqlQuery, player);
            }
        }

        public void AddCard(Player player, Card card)
        {
            PlayerHand playerHand = new PlayerHand() { PlayerId = player.Id, CardId = card.Id };
            using (IDbConnection db = new SqlConnection(AppStrings.connectionString))
            {
                var sqlQuery = "INSERT INTO PlayerHands (PlayerId, CardId) VALUES(@PlayerId, @CardId)";
                db.Execute(sqlQuery, playerHand);
            }
        }

        public ICollection<Card> GetCards(Player player)
        {
            List<Card> cards;
            using (IDbConnection db = new SqlConnection(AppStrings.connectionString))
            {
                var sqlQuery = "SELECT c.* FROM playerhands ph JOIN Cards c ON ( ph.CardId = c.Id ) JOIN Players p ON ( ph.PlayerId = p.Id ) WHERE ph.PlayerId = @Id";
                cards = db.Query<Card>(sqlQuery, player).ToList();
            }
            return cards;
        }
    }
}

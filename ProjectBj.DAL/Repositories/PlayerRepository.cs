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
using ProjectBj.DAL.Interfaces;
using ProjectBj.Configuration;

namespace ProjectBj.DAL.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        public Player Create(Player player)
        {
            using (IDbConnection db = new SqlConnection(Strings.connectionString))
            {
                var sqlQuery = SqlQueries.Players.insert;
                int? userId = db.Query<int>(sqlQuery, player).FirstOrDefault();
            }
            return player;
        }

        public void Delete(int id)
        {
            using(IDbConnection db = new SqlConnection(Strings.connectionString))
            {
                var sqlQuery = SqlQueries.Players.delete;
                db.Execute(sqlQuery, new { id });
            }
        }

        public ICollection<Player> FindPlayers(string name)
        {
            List<Player> players;
            using (IDbConnection db = new SqlConnection(Strings.connectionString))
            {
                var sqlQuery = SqlQueries.Players.find;
                players = db.Query<Player>(sqlQuery, new { name }).ToList();
            }
            return players;
        }

        public Player Get(int id)
        {
            Player player;
            using (IDbConnection db = new SqlConnection(Strings.connectionString))
            {
                var sqlQuery = SqlQueries.Players.select;
                player = db.Query<Player>(sqlQuery, new { id }).FirstOrDefault();
            }
            return player;
        }

        public Player Get(Player player)
        {
            using (IDbConnection db = new SqlConnection(Strings.connectionString))
            {
                var sqlQuery = SqlQueries.Players.get;
                player = db.Query<Player>(sqlQuery, player).FirstOrDefault();
            }
            return player;
        }

        public ICollection<Player> GetAllPlayers()
        {
            List<Player> players;
            using (IDbConnection db = new SqlConnection(Strings.connectionString))
            {
                var sqlQuery = SqlQueries.Players.getAll;
                players = db.Query<Player>(sqlQuery).ToList();
            }
            return players;
        }

        public void Update(Player player)
        {
            using (IDbConnection db = new SqlConnection(Strings.connectionString))
            {
                var sqlQuery = SqlQueries.Players.update;
                db.Execute(sqlQuery, player);
            }
        }

        public void AddCard(Player player, Card card)
        {
            PlayerHand playerHand = new PlayerHand() { PlayerId = player.Id, CardId = card.Id };
            using (IDbConnection db = new SqlConnection(Strings.connectionString))
            {
                var sqlQuery = SqlQueries.Players.addCard;
                db.Execute(sqlQuery, playerHand);
            }
        }

        public ICollection<Card> GetCards(Player player)
        {
            List<Card> cards;
            using (IDbConnection db = new SqlConnection(Strings.connectionString))
            {
                var sqlQuery = SqlQueries.Players.getCards;
                cards = db.Query<Card>(sqlQuery, player).ToList();
            }
            return cards;
        }
    }
}

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
    public class CardRepository : ICardRepository
    {
        string _connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public Card Create(Card card)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = SqlQueries.Cards.insert;
                int? userId = db.Query<int>(sqlQuery, card).FirstOrDefault();
            }
            return card;
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = SqlQueries.Cards.delete;
                db.Execute(sqlQuery, new { id });
            }
        }

        public Card Get(int id)
        {
            Card card;
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = SqlQueries.Cards.select;
                card = db.Query<Card>(sqlQuery, new { id }).FirstOrDefault();
            }
            return card;
        }

        public ICollection<Card> GetAllCards()
        {
            List<Card> cards;
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = SqlQueries.Cards.getAll;
                cards = db.Query<Card>(sqlQuery).ToList();
            }
            return cards;
        }

        public void Update(Card card)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = SqlQueries.Cards.update;
                db.Execute(sqlQuery, card);
            }
        }
    }
}

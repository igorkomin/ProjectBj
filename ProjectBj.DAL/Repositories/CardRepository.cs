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
    public class CardRepository
    {
        public Card Create(Card card)
        {
            using (IDbConnection db = new SqlConnection(AppStrings.connectionString))
            {
                var sqlQuery = "INSERT INTO Cards (Suit, Rank, Value) VALUES(@Suit, @Rank, @Value); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? userId = db.Query<int>(sqlQuery, card).FirstOrDefault();
            }
            return card;
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(AppStrings.connectionString))
            {
                var sqlQuery = "DELETE FROM Cards WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public Card Get(int id)
        {
            Card card;
            using (IDbConnection db = new SqlConnection(AppStrings.connectionString))
            {
                var sqlQuery = "SELECT * FROM Cards WHERE Id = @id";
                card = db.Query<Card>(sqlQuery, new { id }).FirstOrDefault();
            }
            return card;
        }

        public ICollection<Card> GetAllCards()
        {
            List<Card> cards;
            using(IDbConnection db = new SqlConnection(AppStrings.connectionString))
            {
                var sqlQuery = "SELECT * FROM Cards";
                cards = db.Query<Card>(sqlQuery).ToList();
            }
            return cards;
        }

        public void Update(Card card)
        {
            using (IDbConnection db = new SqlConnection(AppStrings.connectionString))
            {
                var sqlQuery = "UPDATE Cards SET Suit = @Suit, Rank = @Rank, Value = @Value WHERE Id = @Id";
                db.Execute(sqlQuery, card);
            }
        }
    }
}

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
    public class CardRepository
    {
        public Card Create(Card card)
        {
            using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
            {
                var sqlQuery = "INSERT INTO Cards (Suit, Rank) " +
                               "VALUES(@Suit, @Rank); " +
                               "SELECT CAST(SCOPE_IDENTITY() as int)";
                int cardId = db.Query<int>(sqlQuery, card).FirstOrDefault();
                card.Id = cardId;
            }
            return card;
        }

        public ICollection<Card> GetAllCards()
        {
            List<Card> cards;
            using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
            { 
                var sqlQuery = "SELECT * FROM Cards";
                cards = db.Query<Card>(sqlQuery).ToList();

            }
            return cards;
        }
    }
}

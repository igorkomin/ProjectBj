using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using Dapper;
using ProjectBj.Common;
using ProjectBj.Common.ExceptionHandlers;
using ProjectBj.Entities;
using ProjectBj.Logger;

namespace ProjectBj.DAL.Repositories
{
    public class CardRepository
    {
        public Card Create(Card card, IDbConnection db)
        {
            var sqlQuery = "INSERT INTO Cards (Suit, Rank) " +
                           "VALUES(@Suit, @Rank); " +
                           "SELECT CAST(SCOPE_IDENTITY() as int)";

            try
            {
                int cardId = db.Query<int>(sqlQuery, card).FirstOrDefault();
                card.Id = cardId;
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }

            return card;
        }

        public ICollection<Card> CreateDeck(ICollection<Card> deck)
        {
            List<Card> newDeck = new List<Card>();
            using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
            {
                foreach (Card card in deck)
                {
                    newDeck.Add(Create(card, db));
                }
            }
            return newDeck;
        }

        public ICollection<Card> GetAllCards()
        {
            List<Card> cards;
            try
            {
                using (IDbConnection db = new SqlConnection(AppStrings.ConnectionString))
                {
                    var sqlQuery = "SELECT * FROM Cards";
                    cards = db.Query<Card>(sqlQuery).AsList();
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
            return cards;
        }
    }
}

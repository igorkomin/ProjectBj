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
using ProjectBj.Logger;
using ProjectBj.DAL.Interfaces;
using ProjectBj.DAL.ExceptionHandlers;

namespace ProjectBj.DAL.Repositories
{
    public class CardRepository : ICardRepository
    {
        private async Task<Card> Create(Card card, IDbConnection db)
        {
            var sqlQuery = "INSERT INTO Cards (Suit, Rank) " +
                           "VALUES(@Suit, @Rank); " +
                           "SELECT CAST(SCOPE_IDENTITY() as int)";

            try
            {
                var cardId = await db.QueryAsync<int>(sqlQuery, card);
                card.Id = cardId.FirstOrDefault();
                return card;
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public ICollection<Card> CreateDeck(ICollection<Card> deck)
        {
            var newDeck = new List<Card>();
            using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
            {
                foreach (Card card in deck)
                {
                    newDeck.Add(Create(card, db));
                }
            }
            return newDeck;
        }

        public async Task<ICollection<Card>> GetAllCards()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var sqlQuery = "SELECT * FROM Cards";
                    var cards = await db.QueryAsync<Card>(sqlQuery);
                    return cards.AsList();
                }
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }
    }
}

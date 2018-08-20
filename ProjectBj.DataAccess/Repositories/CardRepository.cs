using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.Entities;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.DataAccess.ExceptionHandlers;

namespace ProjectBj.DataAccess.Repositories
{
    public class CardRepository : ICardRepository
    {
        private async Task<Card> Create(Card card, IDbConnection db)
        {
            try
            {
                card.Id = await db.InsertAsync(card);
                return card;
            }
            catch (SqlException exception)
            {
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<ICollection<Card>> CreateDeck(ICollection<Card> deck)
        {
            var newDeck = new List<Card>();
            using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
            {
                foreach (Card card in deck)
                {
                    newDeck.Add(await Create(card, db));
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

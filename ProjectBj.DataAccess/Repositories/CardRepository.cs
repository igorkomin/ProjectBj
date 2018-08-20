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
        public async Task<ICollection<Card>> CreateDeck(ICollection<Card> deck)
        {
            using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
            {
                await db.InsertAsync(deck);
            }
            return deck;
        }

        public async Task<ICollection<Card>> GetAllCards()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    var cards = await db.GetAllAsync<Card>();
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

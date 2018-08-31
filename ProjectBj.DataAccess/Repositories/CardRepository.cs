using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.DataAccess.ExceptionHandlers;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Logger;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly string _connectionString;

        public CardRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<ICollection<Card>> CreateDeck(ICollection<Card> deck)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.InsertAsync(deck);
            }
            return deck;
        }

        public async Task<ICollection<Card>> GetAllCards()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var cards = await db.GetAllAsync<Card>();
                    return cards.AsList();
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }
    }
}

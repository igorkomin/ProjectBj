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

        public async Task<ICollection<Card>> GetCards(int playerId, int sessionId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sqlQuery = "SELECT c.* FROM PlayerHands ph " +
                                   "JOIN Cards c ON ( ph.CardId = c.Id ) " +
                                   "WHERE ph.PlayerId = @playerId " +
                                   "AND ph.SessionId = @sessionId";
                    var cards = await db.QueryAsync<Card>(sqlQuery, new { playerId, sessionId });
                    return cards.AsList();
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
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

        public async Task ClearPlayerHand(int playerId, int sessionId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sqlQuery = "DELETE FROM PlayerHands WHERE PlayerId = @playerId AND SessionId = @sessionId";
                    await db.QueryAsync(sqlQuery, new { playerId, sessionId });
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

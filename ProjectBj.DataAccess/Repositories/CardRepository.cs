using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
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

        public async Task<ICollection<Card>> Insert(ICollection<Card> deck)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.InsertAsync(deck);
            }
            return deck;
        }

        public async Task<ICollection<Card>> Get(int playerId, int sessionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"SELECT c.* FROM PlayerHands ph
                                    JOIN Cards c ON (ph.CardId = c.Id)
                                    WHERE ph.PlayerId = @playerId AND ph.SessionId = @sessionId";
                IEnumerable<Card> cards = await db.QueryAsync<Card>(sqlQuery, new { playerId, sessionId });
                return cards.AsList();
            }
        }

        public async Task<ICollection<Card>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<Card> cards = await db.GetAllAsync<Card>();
                return cards.AsList();
            }
        }

        public async Task DeletePlayerHand(int playerId, int sessionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"DELETE FROM PlayerHands 
                                    WHERE PlayerId = @playerId 
                                    AND SessionId = @sessionId";
                await db.QueryAsync(sqlQuery, new { playerId, sessionId });
            }
        }
    }
}

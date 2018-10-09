using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.DataAccess.Repositories.Interfaces;
using ProjectBj.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        private readonly string _connectionString;

        public CardRepository(string connectionString): base(connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Card>> Insert(IEnumerable<Card> deck)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.InsertAsync(deck);
            }
            return deck;
        }

        public async Task<IEnumerable<Card>> Get(long playerId, long sessionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"SELECT c.* FROM PlayerHands ph
                                    JOIN Cards c ON (ph.CardId = c.Id)
                                    WHERE ph.PlayerId = @playerId AND ph.SessionId = @sessionId";
                IEnumerable<Card> cards = await db.QueryAsync<Card>(sqlQuery, new { playerId, sessionId });
                return cards;
            }
        }

        public async Task<IEnumerable<Card>> GetRandom(int count)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT TOP(@count) * FROM Cards ORDER BY NEWID()";
                IEnumerable<Card> cards = await db.QueryAsync<Card>(sqlQuery, new { count });
                return cards;
            }
        }

        public async Task DeletePlayerHand(long playerId, long sessionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"DELETE FROM PlayerHands 
                                    WHERE PlayerId = @playerId 
                                    AND SessionId = @sessionId";
                await db.QueryAsync(sqlQuery, new { playerId, sessionId });
            }
        }
    }
}

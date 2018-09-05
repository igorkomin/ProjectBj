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
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"SELECT c.* FROM PlayerHands ph 
                                     JOIN Cards c ON (ph.CardId = c.Id) 
                                     WHERE ph.PlayerId = @playerId AND ph.SessionId = @sessionId";
                var cards = await db.QueryAsync<Card>(sqlQuery, new { playerId, sessionId });
                return cards.AsList();
            }
        }

        public async Task<ICollection<Card>> GetAllCards()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var cards = await db.GetAllAsync<Card>();
                return cards.AsList();
            }
        }

        public async Task ClearPlayerHand(int playerId, int sessionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var playerHand = (await db.GetAllAsync<PlayerHand>()).AsList()
                    .FindAll(x => x.PlayerId == playerId && x.SessionId == sessionId);
                await db.DeleteAsync(playerHand);
            }
        }
    }
}

using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.DataAccess.Repositories.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Entities.Enums;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories
{
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        private readonly string _connectionString;

        public PlayerRepository(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Player>> GetByType(PlayerType playerType)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "SELECT * FROM Players WHERE Type = @playerType";
                IEnumerable<Player> players = await db.QueryAsync<Player>(sqlQuery, new {playerType});
                return players;
            }
        }

        public async Task<IEnumerable<Player>> Find(string name)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "SELECT * FROM Players WHERE Name = @name";
                IEnumerable<Player> players = await db.QueryAsync<Player>(sqlQuery, new { name });
                return players;
            }
        }

        public async Task AddCardsToPlayerHand(long playerId, IEnumerable<long> cardIds, long sessionId)
        {
            List<PlayerHand> playerHands = new List<PlayerHand>();
            foreach (var id in cardIds)
            {
                var playerHand = new PlayerHand
                {
                    PlayerId = playerId,
                    CardId = id,
                    SessionId = sessionId
                };
                playerHands.Add(playerHand);
            }
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.InsertAsync(playerHands);
            }
        }

        public async Task<IEnumerable<Player>> GetBots(int number)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"SELECT TOP(@number) * FROM Players WHERE Type = 2 
                                    ORDER BY NEWID();";
                IEnumerable<Player> bots = await db.QueryAsync<Player>(sqlQuery, new { number });
                return bots;
            }
        }

        public async Task<IEnumerable<Player>> GetSessionBots(long sessionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"SELECT DISTINCT p.* FROM PlayerHands ph
                                    JOIN Players p ON (ph.PlayerId = p.Id)
                                    WHERE ph.SessionId = @sessionId
                                    AND p.Type = 2";
                IEnumerable<Player> bots = await db.QueryAsync<Player>(sqlQuery, new { sessionId });
                return bots;
            }
        }
    }
}

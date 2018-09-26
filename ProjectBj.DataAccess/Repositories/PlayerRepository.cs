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
    public class PlayerRepository : IPlayerRepository
    {
        private readonly string _connectionString;

        public PlayerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Player> Insert(Player player)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.InsertAsync(player);
                return player;
            }
        }

        public async Task<IEnumerable<Player>> Insert(IEnumerable<Player> players)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.InsertAsync(players);
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

        public async Task<Player> GetById(long id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Player player = await db.GetAsync<Player>(id);
                return player;
            }
        }

        public async Task Update(Player player)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.UpdateAsync(player);
            }
        }

        public async Task AddCardToPlayerHand(Player player, long cardId, long sessionId)
        {
            var playerHand = new PlayerHand
            {
                PlayerId = player.Id,
                CardId = cardId,
                SessionId = sessionId
            };
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.InsertAsync(playerHand);
            }
        }

        public async Task<IEnumerable<Player>> GetSessionBots(long sessionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"SELECT DISTINCT p.* FROM PlayerHands ph
                                    JOIN Players p ON (ph.PlayerId = p.Id)
                                    WHERE ph.SessionId = @sessionId
                                    AND p.IsHuman = 0 AND p.InGame = 1";
                IEnumerable<Player> bots = await db.QueryAsync<Player>(sqlQuery, new { sessionId });
                return bots;
            }
        }

        public async Task DeleteBotsFromSession(long sessionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<Player> sessionBots = await GetSessionBots(sessionId);
                await db.DeleteAsync(sessionBots);
            }
        }
    }
}

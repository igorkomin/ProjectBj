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

        public async Task<ICollection<Player>> Insert(ICollection<Player> players)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.InsertAsync(players);
                return players;
            }
        }

        public async Task<ICollection<Player>> Find(string name)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT * FROM Players WHERE Name = @name";
                var players = await db.QueryAsync<Player>(sqlQuery, new { name });
                return players.AsList() ;
            }
        }

        public async Task<Player> GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var player = await db.GetAsync<Player>(id);
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

        public async Task AddCardToPlayerHand(Player player, int cardId, int sessionId)
        {
            PlayerHand playerHand = new PlayerHand
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

        public async Task<ICollection<Player>> GetSessionBots(int sessionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"SELECT DISTINCT p.* FROM PlayerHands ph
                                     JOIN Players p ON ( ph.PlayerId = p.Id )
                                     WHERE ph.SessionId = @sessionId
                                     AND p.IsHuman = 0 AND p.InGame = 1";
                var bots = await db.QueryAsync<Player>(sqlQuery, new { sessionId });
                return bots.AsList();
            }
        }

        public async Task DeleteBotsFromSession(int sessionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sessionBots = await GetSessionBots(sessionId);
                await db.DeleteAsync(sessionBots);
            }
        }
    }
}

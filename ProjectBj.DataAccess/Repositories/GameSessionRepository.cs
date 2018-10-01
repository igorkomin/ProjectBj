using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.DataAccess.Repositories.Interfaces;
using ProjectBj.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories
{
    public class GameSessionRepository : RepositoryBase<GameSession>, IGameSessionRepository
    {
        private readonly string _connectionString;

        public GameSessionRepository(string connectionString): base(connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<GameSession> GetFirstOpen(long playerId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"SELECT DISTINCT gs.* FROM PlayerHands ph
                                    JOIN GameSessions gs ON (ph.SessionId = gs.Id)
                                    WHERE gs.IsOpen = 1 AND ph.PlayerId = @playerId";

                IEnumerable<GameSession> session = await db.QueryAsync<GameSession>(sqlQuery, new { playerId });
                return session.FirstOrDefault();
            }
        }
    }
}

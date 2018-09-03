using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.DataAccess.ExceptionHandlers;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Logger;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories
{
    public class GameSessionRepository : IGameSessionRepository
    {
        private readonly string _connectionString;

        public GameSessionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<GameSession> Create(GameSession session)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    session.Id = await db.InsertAsync(session);
                    return session;
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<GameSession> GetById(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var session = await db.GetAsync<GameSession>(id);
                    return session;
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task Update(GameSession session)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    await db.UpdateAsync(session);
                }
            }
            catch (SqlException exception)
            {
                Log.Error(exception.Message);
                throw new DataSourceException(exception.Message, exception);
            }
        }

        public async Task<GameSession> GetUnfinishedSession(int playerId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sqlQuery = "SELECT DISTINCT gs.* FROM PlayerHands ph " +
                                   "JOIN Players p ON ( ph.PlayerId = p.Id ) " +
                                   "JOIN GameSessions gs ON ( ph.SessionId = gs.Id ) " +
                                   $"WHERE gs.IsOpen = 1 AND p.Id = {playerId}";

                    var session = await db.QueryAsync<GameSession>(sqlQuery);
                    return session.FirstOrDefault();
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

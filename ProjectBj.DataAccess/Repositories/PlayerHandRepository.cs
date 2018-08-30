using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using ProjectBj.DataAccess.ExceptionHandlers;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Logger;

namespace ProjectBj.DataAccess.Repositories
{
    public class PlayerHandRepository : IPlayerHandRepository
    {
        private string _connectionString;

        public PlayerHandRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddCard(int playerId, int cardId, int sessionId)
        {
            try
            {
                PlayerHand playerHand = new PlayerHand
                {
                    CardId = cardId,
                    PlayerId = playerId,
                    SessionId = sessionId
                };
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    await db.InsertAsync(playerHand);
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

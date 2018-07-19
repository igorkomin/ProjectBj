using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using Dapper;
using ProjectBj.Entities;
using ProjectBj.Configuration;
using ProjectBj.Logger;

namespace ProjectBj.DAL.Repositories
{
    public class CardRepository
    {
        IDbConnection _db;

        public Card Create(Card card)
        {
            try
            {
                _db = new SqlConnection(AppStrings.ConnectionString);
                var sqlQuery = "INSERT INTO Cards (Suit, Rank) VALUES(@Suit, @Rank); SELECT CAST(SCOPE_IDENTITY() as int)";
                int cardId = _db.Query<int>(sqlQuery, card).FirstOrDefault();
                card.Id = cardId;
            }
            catch(Exception exception)
            {
                Log.ToDebug(exception.Message);
                throw;
            }
            finally
            {
                _db.Close();
            }
            return card;
        }

        public ICollection<Card> GetAllCards()
        {
            List<Card> cards;
            try
            {
                _db = new SqlConnection(AppStrings.ConnectionString);
                var sqlQuery = "SELECT * FROM Cards";
                cards = _db.Query<Card>(sqlQuery).ToList();

            }
            catch(Exception exception)
            {
                Log.ToDebug(exception.Message);
                throw;
            }
            finally
            {
                _db.Close();
            }
            return cards;
        }
    }
}

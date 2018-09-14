using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface ICardRepository
    {
        Task<ICollection<Card>> Insert(ICollection<Card> deck);
        Task<ICollection<Card>> Get(int playerId, int sessionId);
        Task<ICollection<Card>> GetAll();
        Task DeletePlayerHand(int playerId, int sessionId);
    }
}
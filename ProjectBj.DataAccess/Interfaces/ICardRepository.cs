using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface ICardRepository
    {
        Task<ICollection<Card>> InsertList(ICollection<Card> deck);
        Task<ICollection<Card>> GetCards(int playerId, int sessionId);
        Task<ICollection<Card>> GetAllCards();
        Task DeletePlayerHand(int playerId, int sessionId);
    }
}
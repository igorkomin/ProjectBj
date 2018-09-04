using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface ICardRepository
    {
        Task<ICollection<Card>> CreateDeck(ICollection<Card> deck);
        Task<ICollection<Card>> GetCards(int playerId, int sessionId);
        Task<ICollection<Card>> GetAllCards();
        Task ClearPlayerHand(int playerId, int sessionId);
    }
}
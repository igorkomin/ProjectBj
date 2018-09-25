using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface ICardProvider
    {
        Task<Card> GetRandomCard();
        Task<IEnumerable<Card>> GetDeck();
        Task<IEnumerable<Card>> GetPlayerHand(long playerId, long sessionId);
        Task ClearPlayerHand(long playerId, long sessionId);
    }
}

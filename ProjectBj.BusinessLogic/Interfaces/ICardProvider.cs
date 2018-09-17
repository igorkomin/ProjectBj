using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface ICardProvider
    {
        Task<List<Card>> GetDeck();
        Task<List<Card>> GetShuffledDeck();
        Task<List<Card>> GetPlayerHand(int playerId, int sessionId);
        Task ClearPlayerHand(int playerId, int sessionId);
    }
}

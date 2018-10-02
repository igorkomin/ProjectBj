using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface ICardProvider
    {
        Task<IEnumerable<Card>> GetPlayerHand(long playerId, long sessionId);
        Task<IEnumerable<Card>> GetRandomCards(int count);
        Task ClearPlayerHand(long playerId, long sessionId);
    }
}

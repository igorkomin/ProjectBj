using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Managers.Interfaces
{
    public interface ICardManager
    {
        Task<IEnumerable<Card>> GetPlayerHand(long playerId, long sessionId);
        Task<IEnumerable<Card>> GetRandomCards(int count);
        Task ClearPlayerHand(long playerId, long sessionId);
    }
}

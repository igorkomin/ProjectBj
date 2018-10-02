using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IPlayerProvider
    {
        Task<Player> GetDealer();
        Task<Player> GetPlayerByName(string name);
        Task<Player> GetPlayerById(long id);
        Task<Player> GetExistingPlayer(string name);
        Task<IEnumerable<Player>> GetBots(int botnumber, long sessionId);
        Task<IEnumerable<Player>> GetSessionBots(long sessionId);
        Task GiveCardsToPlayer(long playerId, long sessionId, IEnumerable<long> cardIds);
        Task DeleteSessionBots(long sessionId);
    }
}

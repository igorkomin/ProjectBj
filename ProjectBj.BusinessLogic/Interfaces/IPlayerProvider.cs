using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IPlayerProvider
    {
        Task<Player> GetDealer();
        Task<Player> GetPlayerByName(string name);
        Task<Player> GetPlayerById(int id);
        Task<Player> GetExistingPlayer(string name);
        Task<List<Player>> GetBots(int botnumber, int sessionId);
        Task<List<Player>> GetSessionBots(int sessionId);
        Task GiveCardToPlayer(int playerId, int sessionId, int cardId);
        Task DeleteSessionBots(int sessionId);
    }
}

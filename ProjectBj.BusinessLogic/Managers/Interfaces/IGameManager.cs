using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.BusinessLogic.Managers.Interfaces
{
    public interface IGameManager
    {
        Task<Game> GetGame(long playerId, long sessionId);
        Task<IEnumerable<Card>> GetCards(long playerId, long sessionId);
        Task<int> GetHandScore(long playerId, long sessionId);
    }
}

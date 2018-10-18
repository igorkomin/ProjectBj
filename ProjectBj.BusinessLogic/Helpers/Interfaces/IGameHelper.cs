using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Helpers.Interfaces
{
    public interface IGameHelper
    {
        Task<Game> GetGame(long playerId, long sessionId);
        Task<IEnumerable<Card>> GetCards(long playerId, long sessionId);
        Task<int> GetHandScore(long playerId, long sessionId);
    }
}

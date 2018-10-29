using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.BusinessLogic.Managers.Interfaces
{
    public interface IGameManager
    {
        Task<(Player player, Player dealer, IEnumerable<Player> bots)> GetAllGamePlayers(long playerId, long sessionId);
        Task<IEnumerable<Card>> GetCards(long playerId, long sessionId);
        Task<int> GetHandScore(long playerId, long sessionId);
    }
}

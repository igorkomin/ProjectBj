using ProjectBj.ViewModels.Game;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IGameServiceHelper
    {
        Task<int> GetHandScore(long playerId, long sessionId);
        Task<ResponseStartGameView> GetStartGameView(long playerId, long sessionId);
        Task<ResponseLoadGameView> GetLoadGameView(long playerId, long sessionId);
        Task<ResponseHitGameView> GetHitGameView(long playerId, long sessionId, bool isLast);
        Task<ResponseStandGameView> GetStandGameView(long playerId, long sessionId);
        Task<ResponseDoubleGameView> GetDoubleGameView(long playerId, long sessionId);
        Task<ResponseSurrenderGameView> GetSurrenderGameView(long playerId, long sessionId);
    }
}

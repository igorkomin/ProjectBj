using ProjectBj.ViewModels.Game;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Helpers.Interfaces
{
    public interface IGameViewHelper
    {
        Task<ResponseStartGameView> GetStartGameView(long playerId, long sessionId);
        Task<ResponseLoadGameView> GetLoadGameView(long playerId, long sessionId);
        Task<ResponseHitGameView> GetHitGameView(long playerId, long sessionId, bool isLastAction);
        Task<ResponseStandGameView> GetStandGameView(long playerId, long sessionId);
        Task<ResponseDoubleGameView> GetDoubleGameView(long playerId, long sessionId);
        Task<ResponseSurrenderGameView> GetSurrenderGameView(long playerId, long sessionId);
    }
}

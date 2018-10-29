using ProjectBj.ViewModels.Game;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Services.Interfaces
{
    public interface IGameService
    {
        Task<ResponseStartGameView> Start(long playerId, int botsNumber);
        Task<ResponseLoadGameView> Load(long playerId);
        Task<ResponseHitGameView> Hit(long playerId, long sessionId);
        Task<ResponseStandGameView> Stand(long playerId, long sessionId);
        Task<ResponseDoubleGameView> Double(long playerId, long sessionId);
        Task<ResponseSurrenderGameView> Surrender(long playerId, long sessionId);
    }
}

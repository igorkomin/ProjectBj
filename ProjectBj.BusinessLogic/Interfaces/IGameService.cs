using ProjectBj.ViewModels.Game;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<ResponseStartGameView> Start(string playerName, int botsNumber);
        Task<ResponseLoadGameView> Load(string playerName);
        Task<ResponseHitGameView> Hit(long playerId, long sessionId);
        Task<ResponseStandGameView> Stand(long playerId, long sessionId);
        Task<ResponseDoubleGameView> Double(long playerId, long sessionId);
        Task<ResponseSurrenderGameView> Surrender(long playerId, long sessionId);
    }
}

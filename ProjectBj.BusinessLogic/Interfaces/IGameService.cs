using ProjectBj.ViewModels.Game;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<ResponseStartGameView> GetNewGame(string playerName, int botsNumber);
        Task<ResponseLoadGameView> GetUnfinishedGame(string playerName);
        Task<ResponseHitGameView> MakeHitDecision(long playerId, long sessionId);
        Task<ResponseStandGameView> MakeStandDecision(long playerId, long sessionId);
        Task<ResponseDoubleGameView> MakeDoubleDownDecision(long playerId, long sessionId);
        Task<ResponseSurrenderGameView> MakeSurrenderDecision(long playerId, long sessionId);
    }
}

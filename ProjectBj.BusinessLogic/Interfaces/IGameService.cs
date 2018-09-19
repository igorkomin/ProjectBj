using ProjectBj.ViewModels.Game;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<ResponseStartGameView> GetNewGame(string playerName, int botsNumber);
        Task<ResponseLoadGameView> GetUnfinishedGame(string playerName);
        Task<ResponseHitGameView> MakeHitDecision(int playerId, int sessionId);
        Task<ResponseStandGameView> MakeStandDecision(int playerId, int sessionId);
        Task<ResponseDoubleGameView> MakeDoubleDownDecision(int playerId, int sessionId);
        Task<ResponseSurrenderGameView> MakeSurrenderDecision(int playerId, int sessionId);
    }
}

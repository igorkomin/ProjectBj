using ProjectBj.ViewModels.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IGameServiceHelper
    {
        Task<int> GetHandScore(int playerId, int sessionId);
        Task<ResponseStartGameView> GetStartGameView(int playerId, int sessionId);
        Task<ResponseLoadGameView> GetLoadGameView(int playerId, int sessionId);
        Task<ResponseHitGameView> GetHitGameView(int playerId, int sessionId, bool isLast);
        Task<ResponseStandGameView> GetStandGameView(int playerId, int sessionId);
        Task<ResponseDoubleGameView> GetDoubleGameView(int playerId, int sessionId);
        Task<ResponseSurrenderGameView> GetSurrenderGameView(int playerId, int sessionId);
    }
}

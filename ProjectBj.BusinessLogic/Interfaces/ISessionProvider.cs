using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface ISessionProvider
    {
        Task<SessionViewModel> CreateSession();
        Task<SessionViewModel> GetSessionByPlayerId(int playerId);
        Task<GameSession> GetSessionById(int id);
        Task CloseSession(int sessionId);
    }
}
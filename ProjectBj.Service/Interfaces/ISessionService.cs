using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.Service.Interfaces
{
    public interface ISessionService
    {
        Task<int> GetSessionByPlayerId(int playerId);
        Task<GameSession> GetSessionById(int id);
        Task CloseSession(int sessionId);
    }
}
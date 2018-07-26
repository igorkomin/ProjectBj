using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.Service.Interfaces
{
    interface ISessionService
    {
        Task<GameSession> CreateSession();
        Task CloseSession(GameSession session);
        Task AddPlayer(GameSession session, Player player);
    }
}
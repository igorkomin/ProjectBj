using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface IGameSessionRepository
    {
        Task<GameSession> GetById(int id);
        Task<GameSession> Create(GameSession session);
        Task<GameSession> GetCurrentSession(int playerId);
        Task Update(GameSession session);
        Task Delete(GameSession session);
    }
}
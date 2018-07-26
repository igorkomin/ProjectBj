using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.DAL.Interfaces
{
    interface IGameSessionRepository
    {
        Task<GameSession> Create(GameSession session);
        Task<GameSession> GetCurrentSession(Player player);
        Task<ICollection<Player>> GetSessionPlayers(GameSession session);
        ICollection<GameSession> GetPlayerSessions(Player player);
        Task Update(GameSession session);
        Task Delete(GameSession session);
        Task AddPlayer(GameSession session, Player player);
    }
}
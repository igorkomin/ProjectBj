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
        GameSession Create(GameSession session);
        GameSession GetCurrentSession(Player player);
        ICollection<Player> GetSessionPlayers(GameSession session);
        ICollection<GameSession> GetPlayerSessions(Player player);
        void Update(GameSession session);
        void Delete(GameSession session);
        void AddPlayer(GameSession session, Player player);
    }
}
﻿using ProjectBj.Entities;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface IGameSessionRepository
    {
        Task<GameSession> GetById(long id);
        Task<GameSession> Insert(GameSession session);
        Task<GameSession> GetFirstOpen(long playerId);
        Task Update(GameSession session);
    }
}
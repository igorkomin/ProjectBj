﻿using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories.Interfaces
{
    public interface ICardRepository : IBaseRepository<Card>
    {
        Task<IEnumerable<Card>> Get(long playerId, long sessionId);
        Task<IEnumerable<Card>> GetRandom(int cardsCount);
        Task DeletePlayerHand(long playerId, long sessionId);
    }
}
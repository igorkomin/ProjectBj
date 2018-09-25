﻿using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> Insert(IEnumerable<Card> deck);
        Task<IEnumerable<Card>> Get(long playerId, long sessionId);
        Task<IEnumerable<Card>> GetAll();
        Task DeletePlayerHand(long playerId, long sessionId);
    }
}
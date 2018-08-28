﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IDeckProvider
    {
        Task<List<Card>> GetDeck();
        Task<List<Card>> GetShuffledDeck();
        Task<CardViewModel> GetCardViewModel(Card card);
        Task<Card> DealCard(int playerId, int sessionId);
        Task DealFirstTwoCards(List<int> players, int sessionId);
    }
}

﻿using ProjectBj.BusinessLogic.Helpers.Interfaces;
using ProjectBj.BusinessLogic.Providers.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Helpers
{
    public class GameHelper : IGameHelper
    {
        private readonly IPlayerProvider _playerProvider;
        private readonly ICardProvider _cardProvider;

        public GameHelper(IPlayerProvider playerProvider, ICardProvider cardProvider)
        {
            _playerProvider = playerProvider;
            _cardProvider = cardProvider;
        }

        public async Task<Game> GetGame(long playerId, long sessionId)
        {
            Player player = await _playerProvider.GetPlayerById(playerId);
            Player dealer = await _playerProvider.GetDealer();
            IEnumerable<Player> bots = await _playerProvider.GetSessionBots(sessionId);

            var game = new Game
            {
                Player = player,
                Dealer = dealer,
                Bots = bots
            };

            return game;
        }

        public async Task<IEnumerable<Card>> GetCards(long playerId, long sessionId)
        {
            IEnumerable<Card> cards = await _cardProvider.GetPlayerHand(playerId, sessionId);
            return cards;
        }

        public async Task<int> GetHandScore(long playerId, long sessionId)
        {
            int totalScore = 0;
            int aceCount = 0;

            IEnumerable<Card> cards = await GetCards(playerId, sessionId);

            foreach (var card in cards)
            {
                int aceCardRank = (int)CardRank.Ace;
                int tenCardRank = (int)CardRank.Ten;

                if ((int)card.Rank == aceCardRank)
                {
                    totalScore += ValueHelper.AceCardValue;
                    aceCount++;
                    continue;
                }
                if ((int)card.Rank > tenCardRank)
                {
                    totalScore += ValueHelper.FaceCardValue;
                    continue;
                }
                totalScore += (int)card.Rank;
            }

            if (totalScore > ValueHelper.BlackjackValue)
            {
                totalScore -= aceCount * ValueHelper.AceDelta;
            }

            return totalScore;
        }
    }
}
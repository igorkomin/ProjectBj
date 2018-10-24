using ProjectBj.BusinessLogic.Managers.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Managers
{
    internal class GameManager : IGameManager
    {
        private readonly IPlayerManager _playerManager;
        private readonly ICardManager _cardManager;

        public GameManager(IPlayerManager playerManager, ICardManager cardManager)
        {
            _playerManager = playerManager;
            _cardManager = cardManager;
        }

        public async Task<Game> GetGame(long playerId, long sessionId)
        {
            Player player = await _playerManager.GetPlayerById(playerId);
            Player dealer = await _playerManager.GetDealer();
            IEnumerable<Player> bots = await _playerManager.GetSessionBots(sessionId);

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
            IEnumerable<Card> cards = await _cardManager.GetPlayerHand(playerId, sessionId);
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
                    totalScore += Constants.AceCardValue;
                    aceCount++;
                    continue;
                }
                if ((int)card.Rank > tenCardRank)
                {
                    totalScore += Constants.FaceCardValue;
                    continue;
                }
                totalScore += (int)card.Rank;
            }

            if (totalScore > Constants.BlackjackValue)
            {
                totalScore -= aceCount * Constants.AceDelta;
            }

            return totalScore;
        }
    }
}
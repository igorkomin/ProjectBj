using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.DataAccess.Repositories;
using ProjectBj.Logger;
using ProjectBj.BusinessLogic.Enums;
using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.ViewModels;
using ProjectBj.ViewModels.Game;

namespace ProjectBj.BusinessLogic.Providers
{
    public class DeckProvider : IDeckProvider
    {
        private List<Card> _deck;
        private ICardRepository _cardRepository;
        private IPlayerRepository _playerRepository;
        private ILogProvider _logProvider;
        
        public DeckProvider(ICardRepository cardRepository, IPlayerRepository playerRepository, ILogProvider logProvider)
        {
            _cardRepository = cardRepository;
            _playerRepository = playerRepository;
            _logProvider = logProvider;
        }

        private List<Card> NewDeck()
        {
            _deck = new List<Card>();

            foreach (var suit in Enum.GetValues(typeof(CardSuits.Suit)))
            {
                AddSuit(suit.ToString());
            }

            return _deck;
        }

        private void AddSuit(string suit)
        {
            foreach (int rank in Enum.GetValues(typeof(CardRanks.Rank)))
            {
                var card = new Card { Rank = rank, Suit = suit };
                _deck.Add(card);
            }
        }
        
        private async Task<List<Card>> PullDeck()
        {
            try
            {
                var deckFromDb = await _cardRepository.GetAllCards();
                if (deckFromDb.ToList().Count == 0)
                {
                    return null;
                }
                return deckFromDb.ToList();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private async Task PushDeck(List<Card> localDeck)
        {
            try
            {
                await _cardRepository.CreateDeck(localDeck);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<List<Card>> GetDeck()
        {
            List<Card> deck = await PullDeck();

            if(deck == null)
            {
                deck = NewDeck();
                await PushDeck(deck);
            }

            return deck;
        }

        private List<Card> Shuffle(List<Card> deck)
        {
            List<Card> shuffledDeck = new List<Card>();
            Random random = new Random(Guid.NewGuid().GetHashCode()); 
            int randomIndex = 0;
            while (deck.Count > 0)
            {
                randomIndex = random.Next(0, deck.Count);
                shuffledDeck.Add(deck[randomIndex]);
                deck.RemoveAt(randomIndex);
            }
            return shuffledDeck;
        }

        public async Task<List<Card>> GetShuffledDeck()
        {
            List<Card> shuffledDeck = Shuffle(await GetDeck());

            return shuffledDeck;
        }

        private async Task GivePlayerCard(Player player, Card card, int sessionId)
        {
            try
            {
                await _playerRepository.AddCard(player, card, sessionId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<Card> DealCard(int playerId, int sessionId)
        {
            Player player = await _playerRepository.GetById(playerId);
            List<Card> deck = await GetShuffledDeck();
            Card card = deck[0];
            await GivePlayerCard(player, card, sessionId);
            CardViewModel cardViewModel = await GetCardViewModel(card);
            await _logProvider.CreateLogEntry(StringHelper.PlayerTakesCard(player.Name, cardViewModel.Rank, cardViewModel.Suit), sessionId);
            return card;
        }

        public async Task DealFirstTwoCards(List<int> playerIds, int sessionId)
        {
            foreach (var playerId in playerIds)
            {
                await DealCard(playerId, sessionId);
                await DealCard(playerId, sessionId);
            }
        }

        public async Task<CardViewModel> GetCardViewModel(Card card)
        {
            CardViewModel cardViewModel = new CardViewModel
            {
                Suit = card.Suit,
                Rank = StringHelper.GetRankName(card.Rank),
                RankValue = card.Rank
            };
            return cardViewModel;
        }
    }
}
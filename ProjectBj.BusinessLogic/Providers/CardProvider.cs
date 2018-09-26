using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.DataAccess.Repositories.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Entities.Enums;
using ProjectBj.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Providers
{
    public class CardProvider : ICardProvider
    {
        private readonly ICardRepository _cardRepository;
        
        public CardProvider(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<IEnumerable<Card>> GetDeck()
        {
            IEnumerable<Card> deck = await GetExistingDeck();
            if (deck == null)
            {
                deck = GetNewDeck();
                await SaveDeck(deck);
                deck = await GetExistingDeck();
            }
            return deck;
        }

        public async Task<Card> GetRandomCard()
        {
            IEnumerable<Card> shuffledDeck = ShuffleDeck(await GetDeck());
            Card card = shuffledDeck.FirstOrDefault();
            return card;
        }

        public async Task<IEnumerable<Card>> GetPlayerHand(long playerId, long sessionId)
        {
            IEnumerable<Card> cards = await _cardRepository.Get(playerId, sessionId);
            return cards;
        }

        public async Task ClearPlayerHand(long playerId, long sessionId)
        {
            await _cardRepository.DeletePlayerHand(playerId, sessionId);
        }

        private IEnumerable<Card> GetNewDeck()
        {
            Log.Info(StringHelper.CreatingDeckMessage);
            var deck = new List<Card>();

            foreach (var suit in Enum.GetValues(typeof(CardSuits.Suit)))
            {
                if ((int)suit != 0)
                {
                    AddCardSuitToDeck(suit.ToString(), deck);
                }
            }

            return deck;
        }

        private void AddCardSuitToDeck(string suit, List<Card> deck)
        {
            foreach (int rank in Enum.GetValues(typeof(CardRanks.Rank)))
            {
                if (rank != 0)
                {
                    var card = new Card { Rank = rank, Suit = suit };
                    deck.Add(card);
                }
            }
        }

        private async Task<IEnumerable<Card>> GetExistingDeck()
        {
            Log.Info(StringHelper.PullingDeckMessage);
            IEnumerable<Card> deck = await _cardRepository.GetAll();
            if (deck.Count() == 0)
            {
                Log.Info(StringHelper.NoDeckInDbMessage);
                return null;
            }
            return deck;
        }

        private async Task SaveDeck(IEnumerable<Card> deck)
        {
            Log.Info(StringHelper.SavingDeckMessage);
            await _cardRepository.Insert(deck);
        }

        private static IEnumerable<Card> ShuffleDeck(IEnumerable<Card> deck)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            IEnumerable<Card> shuffledDeck = deck.OrderBy(item => random.Next(0, deck.Count())).ToList();
            Log.Info(StringHelper.DeckShuffledMessage);
            return shuffledDeck;
        }
    }
}
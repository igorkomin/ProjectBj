using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.DataAccess.Interfaces;
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

        public async Task<List<Card>> GetDeck()
        {
            List<Card> deck = await GetExistingDeck();
            if (deck == null)
            {
                deck = GetNewDeck();
                await SaveDeck(deck);
            }
            return deck;
        }

        public async Task<List<Card>> GetShuffledDeck()
        {
            List<Card> shuffledDeck = ShuffleDeck(await GetDeck());
            return shuffledDeck;
        }

        public async Task<List<Card>> GetPlayerHand(int playerId, int sessionId)
        {
            ICollection<Card> cards = await _cardRepository.Get(playerId, sessionId);
            return cards.ToList();
        }

        public async Task ClearPlayerHand(int playerId, int sessionId)
        {
            await _cardRepository.DeletePlayerHand(playerId, sessionId);
        }

        private List<Card> GetNewDeck()
        {
            Log.Info(StringHelper.CreatingDeckMessage);
            var deck = new List<Card>();

            foreach (var suit in Enum.GetValues(typeof(CardSuits.Suit)))
            {
                AddCardSuitToDeck(suit.ToString(), deck);
            }

            return deck;
        }

        private void AddCardSuitToDeck(string suit, List<Card> deck)
        {
            foreach (int rank in Enum.GetValues(typeof(CardRanks.Rank)))
            {
                var card = new Card { Rank = rank, Suit = suit };
                deck.Add(card);
            }
        }

        private async Task<List<Card>> GetExistingDeck()
        {
            Log.Info(StringHelper.PullingDeckMessage);
            ICollection<Card> deck = await _cardRepository.GetAll();
            if (deck.ToList().Count == 0)
            {
                Log.Info(StringHelper.NoDeckInDbMessage);
                return null;
            }
            return deck.ToList();
        }

        private async Task SaveDeck(List<Card> deck)
        {
            Log.Info(StringHelper.SavingDeckMessage);
            await _cardRepository.Insert(deck);
        }

        private static List<Card> ShuffleDeck(List<Card> deck)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            List<Card> shuffledDeck = deck.OrderBy(item => random.Next(0, deck.Count)).ToList();
            Log.Info(StringHelper.DeckShuffledMessage);
            return shuffledDeck;
        }
    }
}
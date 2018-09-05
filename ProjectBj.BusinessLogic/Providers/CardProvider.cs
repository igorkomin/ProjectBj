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
        private List<Card> _deck;
        private readonly ICardRepository _cardRepository;
        
        public CardProvider(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        private List<Card> NewDeck()
        {
            Log.Info(StringHelper.CreatingDeck);
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
            Log.Info(StringHelper.PullingDeck);
            var deckFromDb = await _cardRepository.GetAllCards();
            if (deckFromDb.ToList().Count == 0)
            {
                Log.Info(StringHelper.NoDeckInDb);
                return null;
            }
            return deckFromDb.ToList();
        }

        private async Task PushDeck(List<Card> localDeck)
        {
            Log.Info(StringHelper.SavingDeck);
            await _cardRepository.CreateDeck(localDeck);
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

        private static List<Card> Shuffle(List<Card> deck)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            List<Card> shuffledDeck = deck.OrderBy(item => random.Next(0, deck.Count)).ToList();
            Log.Info(StringHelper.DeckShuffled);
            return shuffledDeck;
        }

        public async Task<List<Card>> GetShuffledDeck()
        {
            List<Card> shuffledDeck = Shuffle(await GetDeck());
            return shuffledDeck;
        }

        public async Task<List<Card>> GetPlayerCards(int playerId, int sessionId)
        {
            var cards = await _cardRepository.GetCards(playerId, sessionId);
            return cards.ToList();
        }

        public async Task ThrowCards(int playerId, int sessionId)
        {
            await _cardRepository.ClearPlayerHand(playerId, sessionId);
        }
    }
}
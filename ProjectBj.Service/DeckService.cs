using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DAL;
using ProjectBj.DAL.Repositories;
using ProjectBj.Configuration;
using ProjectBj.Logger;

namespace ProjectBj.Service
{
    public static class DeckService
    {
        private static List<Card> _deck;
        private static CardRepository _cardRepository;
        private static PlayerRepository _playerRepository;
        private static Random _random;

        static DeckService()
        {
            _cardRepository = new CardRepository();
            _playerRepository = new PlayerRepository();
            _random = new Random();
        }

        public static List<Card> NewDeck()
        {
            _deck = new List<Card>();
            _deck = Enum.GetValues(typeof(Enums.CardSuitEnum.Suit)).Cast<Enums.CardSuitEnum.Suit>().ToArray().SelectMany(
                suit => Enumerable.Range(1, 12), 
                (suit, rank) => new Card
                {
                    Suit = suit.ToString(),
                    Rank = rank,
                    Value = rank < (int)Enums.CardRanks.Rank.Jack ? rank : GetCardValue(rank)
                }).ToList();


            return _deck;
        }

        private static int GetCardValue(int cardRank)
        {
            if(cardRank == (int)Enums.CardRanks.Rank.Ace)
            {
                return 11;
            }
            return 10;
        }

        public static List<Card> PullDeck()
        {
            List<Card> deckFromDb = _cardRepository.GetAllCards().ToList();

            if (deckFromDb.Count == 0)
            {
                return null;
            }

            return deckFromDb;
        }

        public static void PushDeck(List<Card> localDeck)
        {
            foreach(var card in localDeck)
            {
                _cardRepository.Create(card);
            }
        }

        public static List<Card> GetDeck()
        {
            List<Card> deck = PullDeck();

            if(deck == null)
            {
                deck = NewDeck();
                PushDeck(deck);
            }

            return deck;
        }

        public static List<Card> Shuffle(List<Card> deck)
        {
            List<Card> shuffledDeck = new List<Card>();
            int randomIndex = 0;
            while (deck.Count > 0)
            {
                randomIndex = _random.Next(0, deck.Count);
                shuffledDeck.Add(deck[randomIndex]);
                deck.RemoveAt(randomIndex);
            }
            return shuffledDeck;
        }

        public static List<Card> GetShuffledDeck()
        {
            List<Card> shuffledDeck = Shuffle(GetDeck());
            return shuffledDeck;
        }

        public static void DealCard(Player player)
        {
            List<Card> deck = GetShuffledDeck();
            Card card = deck[0];
            GivePlayerCard(player, card);
            Log.ToDebug(AppStrings.PlayerTakesCard(player.Name, card.Rank, card.Suit));            
        }

        public static void GivePlayerCard(Player player, Card card)
        {
            _playerRepository.AddCard(player, card);
        }
    }
}
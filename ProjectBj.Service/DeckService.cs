using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DAL;
using ProjectBj.DAL.Repositories;
using ProjectBj.ConstantHelper;
using ProjectBj.ConstantHelper.Enums;

namespace ProjectBj.Service
{
    public static class DeckService
    {
        public static List<Card> _deck;
        private static EFUnitOfWork _database;

        static DeckService()
        {
            _database = new EFUnitOfWork();
        }

        public static List<Card> NewDeck()
        {
            _deck = new List<Card>();
            foreach (var suit in Enum.GetValues(typeof(SuitEnum.Suit)))
            {
                AddSuit(suit.ToString());
            }
            return _deck;
        }

        public static List<Card> PullDeck()
        {
            List<Card> deckFromDb = _database.Cards.GetAll().ToList();

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
                _database.Cards.Create(card);
            }
            _database.Save();
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

        private static void AddSuit(string suit)
        {
            foreach (var rank in Values.cardValues)
            {
                int value = CardService.GetCardValue(rank.Key);
                _deck.Add(new Card(rank.Key, suit, value));
            }
        }

        public static List<Card> Shuffle(List<Card> deck)
        {
            List<Card> shuffledDeck = new List<Card>();
            Random r = new Random();
            int randomIndex = 0;
            while (deck.Count > 0)
            {
                randomIndex = r.Next(0, deck.Count);
                shuffledDeck.Add(deck[randomIndex]);
                deck.RemoveAt(randomIndex);
            }
            return shuffledDeck;
        }


    }
}

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
using ProjectBj.StringHelper;
using ProjectBj.Logger;

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
            _deck = Strings.suits.SelectMany(
                suit => Enumerable.Range(0, 13), 
                (suit, rank) => new Card()
                {
                    Suit = suit,
                    Rank = rank,
                    Value = CardService.GetCardValue(rank)
                }).ToList();
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

        public static List<Card> GetShuffledDeck()
        {
            List<Card> shuffledDeck = Shuffle(GetDeck());
            return shuffledDeck;
        }

        public static void DealCard(Player player, bool dealer)
        {
            List<Card> deck = GetShuffledDeck();
            Card card = deck[0];
            if (!dealer)
            {
                GivePlayerCard(player, card);
                Log.ToDebug(Strings.PlayerTakesCard(player.Name, card.Rank));
                return;
            }
            player.Cards.Add(card);
            Log.ToDebug(Strings.DealerTakesCard(card.Rank));
        }

        public static void GivePlayerCard(Player player, Card card)
        {
            _database.Players.Attach(player);
            _database.Cards.Attach(card);

            player.Cards.Add(card);
            _database.Save();

            _database.Players.Detach(player);
            _database.Cards.Detach(card);
        }
    }
}
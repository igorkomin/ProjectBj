﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DAL;
using ProjectBj.DAL.Repositories;
using ProjectBj.Logger;

namespace ProjectBj.Service
{
    public static class DeckService
    {
        private static List<Card> _deck;
        private static CardRepository _cardRepository;
        private static PlayerRepository _playerRepository;
        
        static DeckService()
        {
            _cardRepository = new CardRepository();
            _playerRepository = new PlayerRepository();
        }

        private static List<Card> NewDeck()
        {
            _deck = new List<Card>();

            foreach (var suit in Enum.GetValues(typeof(Enums.CardSuits.Suit)))
            {
                AddSuit(suit.ToString());
            }

            return _deck;
        }

        private static void AddSuit(string suit)
        {
            foreach (int rank in Enum.GetValues(typeof(Enums.CardRanks.Rank)))
            {
                var card = new Card { Rank = rank, Suit = suit };
                _deck.Add(card);
            }
        }
        
        private static List<Card> PullDeck()
        {
            List<Card> deckFromDb;
            try
            {
                deckFromDb = _cardRepository.GetAllCards().ToList();
                if (deckFromDb.Count == 0)
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return deckFromDb;
        }

        private static void PushDeck(List<Card> localDeck)
        {
            try
            {
                _cardRepository.CreateDeck(localDeck);
            }
            catch (Exception exception)
            {
                throw exception;
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

        private static List<Card> Shuffle(List<Card> deck)
        {
            List<Card> shuffledDeck = new List<Card>();
            Random random = new Random();
            int randomIndex = 0;
            while (deck.Count > 0)
            {
                randomIndex = random.Next(0, deck.Count);
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

        public static void GivePlayerCard(Player player, Card card)
        {
            try
            {
                _playerRepository.AddCard(player, card);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
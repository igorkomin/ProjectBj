﻿using ProjectBj.BusinessLogic.Enums;
using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
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
            try
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
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                throw exception;
            }
        }

        private async Task PushDeck(List<Card> localDeck)
        {
            try
            {
                Log.Info(StringHelper.SavingDeck);
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
            Log.Info(StringHelper.DeckShuffled);
            return shuffledDeck;
        }

        public async Task<List<Card>> GetShuffledDeck()
        {
            List<Card> shuffledDeck = Shuffle(await GetDeck());

            return shuffledDeck;
        }
    }
}
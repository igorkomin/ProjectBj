﻿using ProjectBj.BusinessLogic.Helpers;
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

        public async Task<IEnumerable<Card>> GetRandomCards(int count)
        {
            if (count == 0)
            {
                throw new ArgumentException(StringHelper.RandomCardsExceptionMessage);
            }

            IEnumerable<Card> cards = await _cardRepository.GetRandom(count);
            if (cards.Count() == 0)
            {
                cards = GetNewDeck();
                await SaveDeck(cards);
                return await GetRandomCards(count);
            }
            return cards;
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

            foreach (CardSuits suit in Enum.GetValues(typeof(CardSuits)))
            {
                if (suit != 0)
                {
                    AddCardSuitToDeck(suit, deck);
                }
            }

            return deck;
        }

        private void AddCardSuitToDeck(CardSuits suit, List<Card> deck)
        {
            foreach (CardRanks rank in Enum.GetValues(typeof(CardRanks)))
            {
                if (rank != 0)
                {
                    var card = new Card { Rank = rank, Suit = suit };
                    deck.Add(card);
                }
            }
        }

        private async Task SaveDeck(IEnumerable<Card> deck)
        {
            Log.Info(StringHelper.SavingDeckMessage);
            await _cardRepository.Insert(deck);
        }
    }
}
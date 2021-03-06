﻿using ProjectBj.BusinessLogic.Managers.Interfaces;
using ProjectBj.DataAccess.Repositories.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Managers
{
    public class CardManager : ICardManager
    {
        private readonly ICardRepository _cardRepository;
        
        public CardManager(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<IEnumerable<Card>> GetRandomCards(int count)
        {
            if (count == 0)
            {
                throw new ArgumentException(UserMessages.RandomCardsExceptionMessage);
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
            var deck = new List<Card>();

            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                if (suit != 0)
                {
                    AddCardSuitToDeck(suit, deck);
                }
            }

            return deck;
        }

        private void AddCardSuitToDeck(CardSuit suit, List<Card> deck)
        {
            foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
            {
                if (rank != 0)
                {
                    var card = new Card { Rank =  rank, Suit = suit };
                    deck.Add(card);
                }
            }
        }

        private async Task SaveDeck(IEnumerable<Card> deck)
        {
            await _cardRepository.Insert(deck);
        }
    }
}
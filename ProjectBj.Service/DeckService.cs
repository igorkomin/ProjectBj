using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DAL;
using ProjectBj.DAL.Repositories;
using ProjectBj.Logger;
using ProjectBj.Service.Enums;
using ProjectBj.Service.Helpers;
using ProjectBj.Service.Interfaces;

namespace ProjectBj.Service
{
    public class DeckService : IDeckService
    {
        private List<Card> _deck;
        private CardRepository _cardRepository;
        private PlayerRepository _playerRepository;
        
        public DeckService()
        {
            _cardRepository = new CardRepository();
            _playerRepository = new PlayerRepository();
        }

        private List<Card> NewDeck()
        {
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
                var deckFromDb = await _cardRepository.GetAllCards();
                if (deckFromDb.ToList().Count == 0)
                {
                    return null;
                }
                return deckFromDb.ToList();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private async Task PushDeck(List<Card> localDeck)
        {
            try
            {
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

        private async Task<List<Card>> Shuffle(List<Card> deck)
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

        public async Task<List<Card>> GetShuffledDeck()
        {
            List<Card> shuffledDeck = await Shuffle(await GetDeck());
            return shuffledDeck;
        }

        public async Task GivePlayerCard(Player player, Card card)
        {
            try
            {
                await _playerRepository.AddCard(player, card);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task FillDealerHand(Player dealer)
        {
            List<Card> deck = await GetShuffledDeck();
            foreach(var card in deck)
            {
                int dealerTotal = await new PlayerService().GetHandTotal(dealer);
                if (dealerTotal > ValueHelper.MinDealerHandValue)
                {
                    return;
                }
                await GivePlayerCard(dealer, card);
            }
        }

        public async Task DealCard(Player player)
        {
            List<Card> deck = await GetShuffledDeck();
            Card card = deck[0];
            await GivePlayerCard(player, card);

            string cardRank = EnumHelper.GetEnumDescription((CardRanks.Rank)card.Rank);
        }

        public async Task DealFirstTwoCards(List<Player> players)
        {
            foreach (var player in players)
            {
                await DealCard(player);
                await DealCard(player);
            }
        }

        public async Task Hit(Player player)
        {
            await DealCard(player);
        }
    }
}
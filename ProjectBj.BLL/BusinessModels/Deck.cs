using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DAL.Repositories;
using ProjectBj.DAL.Utility;
using ProjectBj.Logger;
using ProjectBj.StringHelper;

namespace ProjectBj.BLL.BusinessModels
{
    public class Deck
    {
        public List<Card> Cards { get; set; }
        private EFUnitOfWork _database;

        public Deck()
        {
            _database = new EFUnitOfWork();
            FillDeck();
        }

        private void FillDeck()
        {
            Cards = _database.Cards.GetAll().ToList();
        }

        public void DealCard(Player player, bool dealer)
        {
            Shuffle();
            Card card = Cards[0];
            if (!dealer)
            {
                DatabaseHelper.GivePlayerCard(player, card);
                Log.ToDebug(Strings.PlayerTakesCard(player.Name, card.Rank));
            }
            else
            {
                player.Cards.Add(card);
                Log.ToDebug(Strings.DealerTakesCard(card.Rank));
            }
        }


        private List<Card> Shuffle(List<Card> deck)
        {
            List<Card> shuffledDeck = new List<Card>();

            Random r = new Random();
            int randomIndex = 0;

            while(deck.Count > 0)
            {
                randomIndex = r.Next(0, deck.Count);
                shuffledDeck.Add(deck[randomIndex]);
                deck.RemoveAt(randomIndex);
            }

            return shuffledDeck;
        }

        public void Shuffle()
        {
            Cards = Shuffle(Cards);
        }
    }
}

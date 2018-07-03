using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.BLL.BusinessModels
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        public Card DealCard(Player player)
        {
            Shuffle();
            return Cards[0];
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

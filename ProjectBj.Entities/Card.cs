using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string Suit { get; set; }
        public string Rank { get; set; }
        public int Value { get; set; }

        public Card()
        {
        }

        public Card(string rank, string suit)
        {
            Suit = suit;
            Rank = rank;
            RankToValue();
        }


        public void RankToValue()
        {
            Value = Values.cardValues[Rank];
        }
    }
    
}

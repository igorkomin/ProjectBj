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
        public ICollection<Player> Players { get; set; }

        public Card(string rank, string suit)
        {
            Suit = suit;
            Rank = rank;
            RankToValue();
        }

        public Card()
        {

        }

        public void RankToValue()
        {
            if (Values.FACES.Contains(Rank))
                Value = 10;
            else if (Rank == Values.ACE)
                Value = 11;
            else
                Value = int.Parse(Rank);
        }
    }
    
}

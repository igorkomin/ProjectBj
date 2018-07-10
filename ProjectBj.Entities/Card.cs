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
        public int Rank { get; set; }
        public int Value { get; set; }
        public ICollection<Player> Players { get; set; }

        public Card()
        {
        }

        public Card(int rank, string suit, int value)
        {
            Suit = suit;
            Rank = rank;
            Value = value;
        }
    }
    
}

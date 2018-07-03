using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Entities
{
    public class Player
    {
        public Player()
        {
            Cards = new List<Card>();
        }

        public Player(string name)
        {
            Name = name;
            Cards = new List<Card>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int PType { get; set; }
        public int Balance { get; set; }
        public bool InGame { get; set; }
        public ICollection<Card> Cards { get; set; }
    }
}

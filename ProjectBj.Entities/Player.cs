using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHuman { get; set; }
        public int Balance { get; set; }
        public bool InGame { get; set; }
        public ICollection<Card> Cards { get; set; }

        public Player()
        {
            Cards = new List<Card>();
        }

        public Player(string name, bool isHuman)
        {
            Name = name;
            IsHuman = isHuman;
            Cards = new List<Card>();
        }
    }
}

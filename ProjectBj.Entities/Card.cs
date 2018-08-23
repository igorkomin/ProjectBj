using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Entities
{
    public class Card : BaseEntity
    {
        public string Suit { get; set; }
        public int Rank { get; set; }
    }
}
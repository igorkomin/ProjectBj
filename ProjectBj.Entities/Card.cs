using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Entities
{
    public class Card
    {
        [Key]
        public int Id { get; set; }

        public string Suit { get; set; }
        public int Rank { get; set; }
    }
}
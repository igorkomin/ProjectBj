using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Entities
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public bool IsHuman { get; set; }
        public int Balance { get; set; }
        public bool InGame { get; set; }
    }
}
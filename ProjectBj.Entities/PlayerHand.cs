using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Entities
{
    public class PlayerHand
    {
        [Key]
        public int Id { get; set; }

        public int PlayerId { get; set; }
        public int CardId { get; set; }
        public int SessionId { get; set; }
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBj.Entities
{
    [Table("Players")]
    public class Player : BaseEntity
    {
        public string Name { get; set; }
        public bool IsHuman { get; set; }
        //public int Balance { get; set; }
        public bool InGame { get; set; }
        //public int Bet { get; set; }
    }
}
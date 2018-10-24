using ProjectBj.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBj.Entities
{
    [Table("Players")]
    public class Player : BaseEntity
    {
        public string Name { get; set; }
        public PlayerType Type { get; set; }
    }
}
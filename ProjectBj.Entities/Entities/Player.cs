using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBj.Entities
{
    [Table("Players")]
    public class Player : BaseEntity
    {
        public string Name { get; set; }
        public bool IsHuman { get; set; }
        public bool InGame { get; set; }
    }
}
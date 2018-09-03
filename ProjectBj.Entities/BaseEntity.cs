using Dapper.Contrib.Extensions;
using System;

namespace ProjectBj.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreationDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        [Computed]
        public DateTime CreationDate { get; set; }
    }
}

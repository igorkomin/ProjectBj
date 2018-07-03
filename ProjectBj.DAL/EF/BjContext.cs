using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ProjectBj.Entities;

namespace ProjectBj.DAL.EF
{
    public class BjContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Card> Cards { get; set; }

        public BjContext()
            : base("DBConnection")
        {
            Database.SetInitializer(new BjDbInitializer());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ProjectBj.Entities;

namespace ProjectBj.DAL.EF
{
    public class BjDbInitializer : DropCreateDatabaseIfModelChanges<BjContext>
    {
        protected override void Seed(BjContext db)
        {     
        }
    }
}

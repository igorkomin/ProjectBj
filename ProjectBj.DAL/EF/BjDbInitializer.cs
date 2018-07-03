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
            List<Card> cards = new List<Card>();

            foreach (var suit in Values.SUITS)
            {
                for (int i = 2; i < 11; i++)
                    db.Cards.Add(new Card(i.ToString(), suit));
                foreach (var face in Values.FACES)
                    db.Cards.Add(new Card(face, suit));
                db.Cards.Add(new Card(Values.ACE, suit));
            }
        }
    }
}

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
            int minCardValue = 2;
            int maxCardValue = 11;

            foreach (var suit in Enum.GetValues(typeof(Values.Suits)))
            {
                for (int i = minCardValue; i < maxCardValue; i++)
                {
                    db.Cards.Add(new Card(i.ToString(), suit.ToString()));
                }
                foreach(var face in Enum.GetValues(typeof(Values.Faces)))
                {
                    db.Cards.Add(new Card(face.ToString(), suit.ToString()));
                }
            }
        }
    }
}

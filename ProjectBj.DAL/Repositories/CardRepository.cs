using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ProjectBj.Entities;
using ProjectBj.DAL.EF;
using ProjectBj.DAL.Interfaces;

namespace ProjectBj.DAL.Repositories
{
    class CardRepository : IRepository<Card>
    {
        private BjContext db;

        public CardRepository(BjContext context)
        {
            this.db = context;
        }

        public void Create(Card card)
        {
            db.Cards.Add(card);
        }

        public void Attach(Card card)
        {
            db.Cards.Attach(card);
        }

        public void Detach(Card card)
        {
            db.Entry<Card>(card).State = EntityState.Detached;
        }

        public void Delete(int id)
        {
            Card card = db.Cards.Find(id);
            if (card != null)
                db.Cards.Remove(card);
        }

        public ICollection<Card> Find(Func<Card, bool> predicate)
        {
            return db.Cards.Include(x => x.Players).Where(predicate).ToList();
        }

        public Card Get(int id)
        {
            return db.Cards.Find(id); 
        }

        public ICollection<Card> GetAll()
        {
            return db.Cards.Include(x => x.Players).ToList();
        }

        public void Update(Card card)
        {
            db.Entry(card).State = EntityState.Modified;
        }
    }
}

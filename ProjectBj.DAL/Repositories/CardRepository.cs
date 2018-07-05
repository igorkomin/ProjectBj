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
        private BjContext _db;

        public CardRepository(BjContext context)
        {
            _db = context;
        }

        public void Create(Card card)
        {
            _db.Cards.Add(card);
        }

        public void Attach(Card card)
        {
            _db.Cards.Attach(card);
        }

        public void Detach(Card card)
        {
            _db.Entry<Card>(card).State = EntityState.Detached;
        }

        public void Delete(int id)
        {
            Card card = _db.Cards.Find(id);
            if (card != null)
                _db.Cards.Remove(card);
        }

        public ICollection<Card> Find(Func<Card, bool> predicate)
        {
            return _db.Cards.Include(x => x.Players).Where(predicate).ToList();
        }

        public Card Get(int id)
        {
            return _db.Cards.Find(id); 
        }

        public ICollection<Card> GetAll()
        {
            return _db.Cards.Include(x => x.Players).ToList();
        }

        public void Update(Card card)
        {
            _db.Entry(card).State = EntityState.Modified;
        }
    }
}

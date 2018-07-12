using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.DAL.Interfaces
{
    interface ICardRepository
    {
        Card Get(int id);
        ICollection<Card> GetAllCards();
        Card Create(Card card);
        void Update(Card card);
        void Delete(int id);
    }
}

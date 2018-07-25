using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.DAL.Interfaces
{
    interface ICardRepository
    {
        ICollection<Card> CreateDeck(ICollection<Card> deck);
        ICollection<Card> GetAllCards();
    }
}
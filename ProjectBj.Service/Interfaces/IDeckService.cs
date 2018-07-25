using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.Service.Interfaces
{
    public interface IDeckService
    {
        List<Card> GetDeck();
        List<Card> GetShuffledDeck();
        void GivePlayerCard(Player player, Card card);
    }
}

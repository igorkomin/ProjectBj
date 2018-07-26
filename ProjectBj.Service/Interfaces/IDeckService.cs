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
        Task<List<Card>> GetDeck();
        Task<List<Card>> GetShuffledDeck();
        Task GivePlayerCard(Player player, Card card);
        Task FillDealerHand(Player player);
        Task DealCard(Player player);
        void Hit(Player player);
        Task DealFirstTwoCards(List<Player> players);
    }
}

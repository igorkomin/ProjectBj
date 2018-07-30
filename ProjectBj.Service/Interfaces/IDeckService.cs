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
        Task DealCard(int playerId, int sessionId);
        Task Hit(int playerId, int sessionId);
        Task DealFirstTwoCards(List<int> players, int sessionId);
    }
}

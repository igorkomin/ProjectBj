using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IDeckProvider
    {
        Task<List<Card>> GetDeck();
        Task<List<Card>> GetShuffledDeck();
    }
}

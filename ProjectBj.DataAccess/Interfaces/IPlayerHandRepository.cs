using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface IPlayerHandRepository
    {
        Task AddCard(int playerId, int cardId, int sessionId);
    }
}

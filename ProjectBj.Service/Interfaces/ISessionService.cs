using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.Service.Interfaces
{
    interface ISessionService
    {
        GameSession CreateSession();
        void CloseSession(GameSession session);
        void AddPlayer(GameSession session, Player player);
    }
}
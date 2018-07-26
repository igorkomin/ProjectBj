using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.Service.Interfaces
{
    interface IGameService
    {
        int GetHandTotal(Player player);
        bool IsBlackjack(int handTotal);
        bool IsBust(int handTotal);
        void Stay(Player player);
    }
}

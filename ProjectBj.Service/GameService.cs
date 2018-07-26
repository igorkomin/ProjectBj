using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.DAL;
using ProjectBj.DAL.Repositories;
using ProjectBj.Entities;
using ProjectBj.Service.Enums;
using ProjectBj.Service.Helpers;
using ProjectBj.Service.Interfaces;

namespace ProjectBj.Service
{
    public class GameService : IGameService
    {
        public bool IsBlackjack(int handTotal)
        {
            bool isBlackJack = handTotal == ValueHelper.BlackjackValue ? true : false;
            return isBlackJack;
        }

        public bool IsBust(int handTotal)
        {
            bool isBust = handTotal > ValueHelper.BlackjackValue ? true : false;
            return isBust;
        }

        public void Stay(Player player)
        {
            player.InGame = false;
        }
    }
}

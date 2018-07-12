using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DAL;
using ProjectBj.DAL.Repositories;
using ProjectBj.Configuration;

namespace ProjectBj.Service
{
    public static class CardService
    {
        public static int GetCardValue(int cardRank)
        {
            int cardValue = Values.cardValues[cardRank];
            return cardValue;
        }
    }
}

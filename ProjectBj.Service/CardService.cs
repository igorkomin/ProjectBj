using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DAL;
using ProjectBj.DAL.Repositories;
using ProjectBj.ConstantHelper;

namespace ProjectBj.Service
{
    public static class CardService
    {
        private static EFUnitOfWork _database;

        static CardService()
        {
            _database = new EFUnitOfWork();
        }

        public static int GetCardValue(string cardRank)
        {
            int cardValue = Values.cardValues[cardRank];
            return cardValue;
        }
    }
}

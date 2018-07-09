using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.DAL;
using ProjectBj.DAL.Repositories;
using ProjectBj.Entities;
using ProjectBj.StringHelper;

namespace ProjectBj.Service
{
    public static class GameService
    {
        public static EFUnitOfWork _database;

        static GameService()
        {
            _database = new EFUnitOfWork();
        }

        public static void GivePlayerCard(Player player, Card card)
        {
            _database.Players.Attach(player);
            _database.Cards.Attach(card);

            player.Cards.Add(card);
            _database.Save();

            _database.Players.Detach(player);
            _database.Cards.Detach(card);
        }

        public static int GetHandTotal(Player player)
        {
            int totalValue = 0;
            int aceCount = 0;

            List<Card> cards = player.Cards.ToList();

            foreach(var card in cards)
            {
                if(card.Rank == Strings.ace)
                {
                    aceCount++;
                }
                totalValue += card.Value;
            }

            return totalValue;
        }
    }
}

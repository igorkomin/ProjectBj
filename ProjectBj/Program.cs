using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DAL.Repositories;
using ProjectBj.DAL.EF;
using ProjectBj.DAL.Utility;
using ProjectBj.BLL.BusinessModels;

namespace ProjectBj
{
    class Program
    {
        static void Main(string[] args)
        {
            
            GameManager manager = new GameManager();

            manager.AddPlayer(new Player("Player1", true));
            manager.DealFirstTwoCards();

            Player dealer = manager.GetDealer();

            List<Player> p = manager.GetPlayers();

            
            manager.FillDealerHand();
            manager.Hit(p[0]);

            
            p = manager.GetPlayers();
            int dealerTotal = manager.GetHandTotal(dealer.Cards.ToList());
            int playerTotal = manager.GetHandTotal(p[0].Cards.ToList());
            
            

            Console.ReadKey();
        }
    }
}

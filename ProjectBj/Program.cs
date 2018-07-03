using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DAL.Repositories;
using ProjectBj.DAL.EF;
using ProjectBj.BLL.BusinessModels;

namespace ProjectBj
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Deck deck = new Deck();
            Player player = new Player("player1");
            
            Game.GiveCardToPlayer(player, deck);
            Game.GiveCardToPlayer(player, deck);

            while (player.InGame)
            {
                ConsoleView.DisplayMessage("You have:");
                foreach (var card in player.Cards)
                {
                    ConsoleView.DisplayMessage($"{card.Rank} of {card.Suit}");
                }
                ConsoleView.DisplayMessage($"TotalValue: {player.Hand.TotalValue()}");

                if (player.Hand.TotalValue() > 21)
                {
                    ConsoleView.DisplayMessage("Bust!");
                    player.IsPlaying = false;
                }
                else if (player.Hand.TotalValue() == 21)
                {
                    ConsoleView.DisplayMessage("Blackjack!");
                    player.IsPlaying = false;
                }


                    if (player.IsPlaying)
                    {
                        ConsoleView.DisplayMessage("Hit or Stay? (Hit - 1, Stay - 2)");
                        player.Response = ConsoleView.ReadData();

                        if (player.Response == "1")
                        {
                            player.IsPlaying = true;
                            Game.GiveCardToPlayer(player, deck);
                        }
                        else
                            player.IsPlaying = false;
                    }
            }*/


            /*EFUnitOfWork ef = new EFUnitOfWork();

            Player p = ef.Players.Get(1);
            Card c = ef.Cards.Get(44);

            p.Cards.Add(c);
            ef.Players.Update(p);
            ef.Save();*/

            
            Console.ReadKey();
        }
    }
}

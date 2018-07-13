using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DAL.Repositories;
using ProjectBj.Service;

namespace ProjectBj
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Card> cards = DeckService.NewDeck();

            //var array = Enum.GetValues(typeof(Service.Enums.CardSuitEnum.Suit))
                //.Cast<Service.Enums.CardSuitEnum.Suit>().ToArray();

            Console.ReadKey();
        }
    }
}

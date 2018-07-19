using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DAL.Repositories;
using ProjectBj.Service;

namespace ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Card> deck = DeckService.GetDeck();
        }
    }
}

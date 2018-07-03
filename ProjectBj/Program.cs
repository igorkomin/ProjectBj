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
            GameSession session = new GameSession();
            Console.ReadKey();
        }
    }
}

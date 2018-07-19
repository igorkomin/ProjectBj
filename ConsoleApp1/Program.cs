using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProjectBj.Entities;
using ProjectBj.Service;
using ProjectBj.DAL.Repositories;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new PlayerRepository();
            Player player = new Player();
            repository.Create(player);
        }
    }
}
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
    public static class PlayerService
    {
        private static EFUnitOfWork _database;

        static PlayerService()
        {
            _database = new EFUnitOfWork();
        }

        public static Player NewPlayer(string name)
        {
            Player player = new Player(name, true);
            return player;
        }

        public static Player NewBot()
        {
            Player newBot = new Player(Strings.botName);
            return newBot;
        }

        public static Player PullPlayer(string name)
        {
            Player player = _database.Players.Find(x => x.Name == name).FirstOrDefault();
            return player;
        }

        public static void PushPlayer(Player localPlayer)
        {
            _database.Players.Create(localPlayer);
            _database.Save();
        }

        public static Player GetPlayer(string name)
        {
            Player player = PullPlayer(name);
            if(player == null)
            {
                player = NewPlayer(name);
                PushPlayer(player);
            }
            return player;
        }

        public static Player GetPlayerById(int playerId)
        {
            Player player = _database.Players.Get(playerId);
            return player;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.DAL.Repositories;
using ProjectBj.Entities;

namespace ProjectBj.DAL.Utility
{
    public static class DatabaseHelper
    {
        private static EFUnitOfWork _database;

        static DatabaseHelper()
        {
            _database = new EFUnitOfWork();
        }

        public static Player GetPlayerById(int id)
        {
            return _database.Players.Get(id);
        }

        public static Player GetPlayerByName(string name)
        {
            return _database.Players.Find(x => x.Name == name).FirstOrDefault();
        }

        public static void RegisterPlayer(Player newPlayer)
        {
            _database.Players.Create(newPlayer);
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

        public static List<Player> SyncPlayers(List<Player> players)
        {
            List<Player> syncedPlayers = new List<Player>();
            foreach (var player in players)
            {
                syncedPlayers.Add(GetPlayerById(player.Id));
            }
            return syncedPlayers;
        }
    }
}

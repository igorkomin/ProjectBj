﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.Service.Interfaces
{
    interface IPlayerService
    {
        Task<Player> GetDealer();
        Player GetPlayer(string name);
        Player GetPlayerById(int id);
        Task<List<Player>> CreateBots(int number);
        List<Card> GetCards(Player player);
        int GetHandTotal(Player player);
        void ThrowCards(Player player);
        void ChangePlayerBalance(Player player, int balanceDelta);
        void DeletePlayer(Player player);
    }
}

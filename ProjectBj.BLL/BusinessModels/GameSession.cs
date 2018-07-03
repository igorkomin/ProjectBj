﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.BLL.BusinessModels
{
    public class GameSession
    {
        private Deck deck = new Deck();
        private List<Player> players = new List<Player>();
        private Player dealer = new Player();

        public void AddPlayer(Player newPlayer)
        {
            players.Add(newPlayer);
        }

        public void DealFirstTwoCards()
        {
            deck.DealCard(dealer);
            deck.DealCard(dealer);

            for(int i = 0; i < players.Count; i++)
            {
                deck.DealCard(players[i]);
                deck.DealCard(players[i]);
            }
        }

        private int GetHandTotal(List<Card> cards)
        {
            int totalValue = 0;
            bool hasAce = false;

            foreach (var card in cards)
            {
                if(card.Rank == Values.ACE)
                {
                    if (!hasAce)
                        hasAce = true;
                    if (hasAce && totalValue > 21)
                        totalValue -= 10;
                }
                    totalValue += card.Value;
            }

            return totalValue;
        }
    }
}

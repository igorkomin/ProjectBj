using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.DAL.Repositories;
using ProjectBj.Entities;
using ProjectBj.Service.Interfaces;
using ProjectBj.Service.Helpers;
using ProjectBj.Service.Enums;

namespace ProjectBj.Service
{
    public class PlayerService : IPlayerService
    {
        private PlayerRepository _playerRepository;
        private CardRepository _cardRepository;

        public PlayerService()
        {
            _playerRepository = new PlayerRepository();
            _cardRepository = new CardRepository();
        }

        private async Task<Player> NewPlayer(string name)
        {
            Player player = new Player { Name = name, Balance = ValueHelper.StartBalance, InGame = true, IsHuman = true };
            try
            {
                player = await _playerRepository.CreateOne(player);
                return player;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private async Task<Player> NewBot()
        {
            Player newBot = new Player { Name = StringHelper.BotName, Balance = ValueHelper.StartBalance, IsHuman = false, InGame = true };
            try
            {
                newBot = await _playerRepository.CreateOne(newBot);
                return newBot;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private async Task<Player> NewDealer()
        {
            Player dealer = new Player { Name = StringHelper.DealerName, InGame = false, IsHuman = false };
            try
            {
                dealer = await _playerRepository.CreateOne(dealer);
                return dealer;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public async Task<List<Player>> CreateBots(int number)
        {
            DeleteAllBots();
            List<Player> bots = new List<Player>();
            for(int i = 0; i < number; i++)
            {
                Player bot = await NewBot();
                bots.Add(bot);
            }
            return bots;
        }

        public async Task<Player> GetDealer()
        {
            Player dealer = await PullPlayer(StringHelper.DealerName);
            if(dealer == null)
            {
                dealer = await NewDealer();
            }
            return dealer;
        }

        private async Task<Player> PullPlayer(string name)
        {
            try
            {
                var player = await _playerRepository.FindPlayers(name);
                return player.FirstOrDefault();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<Player> GetPlayer(string name)
        {
            Player player = await PullPlayer(name);
            if(player == null)
            {
                player = await NewPlayer(name);
            }
            return player;
        }

        public async Task<Player> GetPlayerById(int id)
        {
            Player player;
            try
            {
                player = await _playerRepository.Get(id);
                return player;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<List<Card>> GetCards(Player player)
        {
            try
            {
                var cards = await _playerRepository.GetCards(player);
                return cards.ToList();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task ThrowCards(Player player)
        {
            try
            {
                await _playerRepository.DeleteCards(player);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task ChangePlayerBalance(Player player, int balanceDelta)
        {
            player.Balance += balanceDelta;
            try
            {
                await _playerRepository.Update(player);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task DeletePlayer(Player player)
        {
            try
            {
                await _playerRepository.Delete(player.Id);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private async Task DeleteAllBots()
        {
            try
            {
                await _playerRepository.DeletePlayersByName(StringHelper.BotName);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public int GetHandTotal(Player player)
        {
            int totalValue = 0;
            int aceCount = 0;

            List<Card> cards = GetCards(player);

            foreach (var card in cards)
            {
                int aceCardRank = (int)CardRanks.Rank.Ace;
                int tenCardRank = (int)CardRanks.Rank.Ten;
                if (card.Rank == aceCardRank)
                {
                    totalValue += ValueHelper.AceCardValue;
                    continue;
                }
                if (card.Rank > tenCardRank)
                {
                    totalValue += ValueHelper.FaceCardValue;
                    continue;
                }
                totalValue += card.Rank;
            }

            return totalValue > ValueHelper.BlackjackValue ? totalValue - aceCount * ValueHelper.AceDelta : totalValue;
        }
    }
}

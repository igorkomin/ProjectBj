using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectBj.ViewModels;
using ProjectBj.Entities;
using ProjectBj.Service;

namespace ProjectBj.Web.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Play(GameSettings settings)
        {
            Player dealer = PlayerService.GetDealer();
            Player humanPlayer = PlayerService.GetPlayer(settings.PlayerName);
            List<Player> players = new List<Player>();
            players.Add(dealer);
            players.Add(humanPlayer);
            players.AddRange(PlayerService.CreateBots(settings.BotsNumber));
            GameService.DealFirstTwoCards(players);
            List<PlayerView> playerViews = new List<PlayerView>();
            foreach(var player in players)
            {
                List<Card> cards = PlayerService.GetCards(player);
                PlayerView playerView = new PlayerView() { Id = player.Id, Balance = player.Balance, InGame = player.InGame, Name = player.Name, IsHuman = player.IsHuman, Cards = cards };
                playerViews.Add(playerView);
            }
            
            return View(playerViews);
        }
    }
}
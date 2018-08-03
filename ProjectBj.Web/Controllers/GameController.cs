using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectBj.ViewModels;
using ProjectBj.ViewModels.Game;
using ProjectBj.Entities;
using ProjectBj.Service;
using ProjectBj.Service.Providers;
using System.Threading.Tasks;

namespace ProjectBj.Web.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Play()
        {
            return View();
        }
    }
}
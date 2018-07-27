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
            return View();
        }
    }
}
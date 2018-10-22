using System.Web.Mvc;

namespace ProjectBj.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/dist");
        }
    }
}
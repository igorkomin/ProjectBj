using System.Web.Mvc;

namespace ProjectBj.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var filePathResult = new FilePathResult("~/dist/index.html", "text/html");
            return filePathResult;
        }
    }
}
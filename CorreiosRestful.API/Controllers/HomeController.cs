using System.Web.Mvc;

namespace CorreiosRestful.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
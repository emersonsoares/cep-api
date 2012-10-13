using System.Web.Mvc;

namespace CorreiosRestful.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Correios RESTful API";
            return View();
        }
    }
}
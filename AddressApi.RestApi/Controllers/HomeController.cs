using System.Web.Mvc;

namespace AddressApi.RestApi.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(Duration = 86400)]
        public ActionResult Index()
        {
            ViewBag.Title = "Correios RESTful API";
            return View();
        }
    }
}

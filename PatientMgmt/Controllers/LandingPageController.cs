using Microsoft.AspNetCore.Mvc;

namespace PatientMgmt.Controllers
{
    public class LandingPageController : Controller
    {
        // GET: LandingPageController
        public ActionResult Index()
        {
            ViewBag.Layout = null;
            return View();
        }

    }
}

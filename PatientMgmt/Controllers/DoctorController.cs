using Microsoft.AspNetCore.Mvc;
using PatientMgmt.Core.Application;

namespace PatientMgmt.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _doctorService.GetAll());
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Engines;
using PatientMgmt.Core.Application;
using PatientMgmt.Core.Application.Interfaces.Services;
using PatientMgmt.Core.Application.ViewModels.TestResults;

namespace PatientMgmt.Controllers
{
    public class LabResultController : Controller
    {
        private readonly ITestResultService _testResultService;
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;

        public LabResultController(ITestResultService testResultService, IAppointmentService appointmentService, IPatientService patientService)
        {
            _testResultService = testResultService;
            _appointmentService = appointmentService;
            _patientService = patientService;
        }

        public async Task<ActionResult> Index(FilterTrByIdViewModel? vm)
        {
            ViewBag.FilterIdCard = vm.IdCard; 
            ViewBag.IsSearch = vm.IdCard.HasValue;
            return View(await _testResultService.GetAllPendingTestResults(vm));
        }

        public async Task<IActionResult> ReportResults(int id)
        {
            SaveTestResultViewModel vm = await _testResultService.GetByIdSaveViewModel(id);
            return View("ReportResult", vm);
        }

        [HttpPost]
        public async Task<IActionResult> ReportResults(SaveTestResultViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("ReportResult", vm);
            }
            SaveTestResultViewModel TrVm = await _testResultService.GetByIdSaveViewModel(vm.Id);
            TrVm.Result = vm.Result;

            await _testResultService.Update(TrVm, vm.Id);
            return RedirectToRoute(new { controller = "LabResult", action = "Index" });
        }

    }
}

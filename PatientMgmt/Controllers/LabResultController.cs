using Microsoft.AspNetCore.Mvc;
using PatientMgmt.Core.Application;
using PatientMgmt.Core.Application.Interfaces.Services;

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

        public ActionResult Index()
        {
            return View();
        }

    }
}

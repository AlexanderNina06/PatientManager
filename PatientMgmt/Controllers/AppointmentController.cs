using Microsoft.AspNetCore.Mvc;
using PatientMgmt.Core.Application;
using PatientMgmt.Core.Application.Interfaces.Services;

namespace PatientMgmt.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        public AppointmentController(IAppointmentService appointmentService, IPatientService patientService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _doctorService = doctorService;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _appointmentService.GetAllViewModelWithInclude());
        }

        public async Task<IActionResult> Create()
        {  
            SaveAppointmentViewModel vm = new();
            vm.Patients = await _patientService.GetAll();
            vm.Doctors = await _doctorService.GetAll();

            return View("SaveAppointment", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SaveAppointmentViewModel vm)
        {  
            if (!ModelState.IsValid){
                return View("SaveAppointment", vm);
            }

            await _appointmentService.Add(vm);
            return RedirectToRoute(new {controller = "Appointment", action = "Index"});
        }

        public async Task<IActionResult> Edit(int id)
        {
            SaveAppointmentViewModel vm = await _appointmentService.GetByIdSaveViewModel(id);
            vm.Patients = await _patientService.GetAll();
            vm.Doctors = await _doctorService.GetAll();
            
            return View("SaveAppointment", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveAppointmentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveAppointment", vm);
            }

            await _appointmentService.Update(vm, vm.Id);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {  
            return View(await _appointmentService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            
            await _appointmentService.Delete(id);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }

    }
}

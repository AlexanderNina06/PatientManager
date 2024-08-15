using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using PatientMgmt.Core.Application;
using PatientMgmt.Core.Application.Interfaces.Services;
using PatientMgmt.Core.Application.ViewModels.Appointments;

namespace PatientMgmt.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly ILabTestService _labTestService;
        public AppointmentController(IAppointmentService appointmentService, IPatientService patientService, IDoctorService doctorService, ILabTestService labTestService)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _doctorService = doctorService;
            _labTestService = labTestService;
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

        public async Task<IActionResult> AssignLabTests(int id)
        {
            var appointment = await _appointmentService.GetByIdSaveViewModel(id);
            if(appointment == null){
                return NotFound();
            }

            AssignLabTestsViewModel vm = new();
            vm.SelectedLabTestIds = new List<int>();
            vm.LabTests = await _labTestService.GetAll();
            vm.AppointmentId = id;

            return View("AssignLabTests", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AssignLabTests(AssignLabTestsViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _appointmentService.AssignLabTestsToAppointment(vm.AppointmentId, vm.SelectedLabTestIds); 
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }

        public async Task<IActionResult> ConsultResultStatus(int id)
        {
            var appointment = await _appointmentService.GetByIdSaveViewModel(id);
            if(appointment == null){
                return NotFound();
            }

            var resultVm = await _appointmentService.ConsutResults(id);
            ViewBag.AppointmentId = id;  
            
            return View("ConsultResultStatus", resultVm);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteAppt(int AppointmentId)
        {
            await _appointmentService.CompleteAppt(AppointmentId);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }

        public async Task<IActionResult> ConsultCompletedAppts(int id)
        {
            var appointment = await _appointmentService.GetByIdSaveViewModel(id);
            if(appointment == null){
                return NotFound();
            }

            var resultVm = await _appointmentService.ConsutResults(id);
            ViewBag.AppointmentId = id;  
        
            return View("ConsultResultStatus", resultVm);
        }

    }
}

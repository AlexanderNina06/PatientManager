using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using PatientMgmt.Core.Application;
using Microsoft.AspNetCore.Authorization;

namespace PatientMgmt.Controllers
{
    [Authorize]
    public class LabTestController : Controller
    {
        private readonly ILabTestService _labTestService;
        public LabTestController(ILabTestService labTestService){
            _labTestService = labTestService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _labTestService.GetAll());
        }

        public IActionResult Create()
        {  
            return View("SaveLabTest", new SaveLabTestViewModel());
        }
        
        [HttpPost]
        public async Task <IActionResult> Create(SaveLabTestViewModel vm)
        {  
            if(!ModelState.IsValid){
                return View("SaveLabTest", vm);
            }
            
            await _labTestService.Add(vm);
            return RedirectToRoute(new {Controller="LabTest", action="Index"});
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveLabTest", await _labTestService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveLabTestViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveLabTest", vm);
            }

            await _labTestService.Update(vm, vm.Id);
            return RedirectToRoute(new { controller = "LabTest", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _labTestService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            
            await _labTestService.Delete(id);
            return RedirectToRoute(new { controller = "LabTest", action = "Index" });
        }

    }
}

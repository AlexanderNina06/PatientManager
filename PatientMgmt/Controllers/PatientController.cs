using Microsoft.AspNetCore.Mvc;
using PatientMgmt.Core.Application;
using Microsoft.AspNetCore.Authorization;

namespace PatientMgmt.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _patientService.GetAll());
        }

        public IActionResult Create()
        {  
            return View("SavePatient", new SavePatientViewModel());
        }
        
        [HttpPost]
        public async Task <IActionResult> Create(SavePatientViewModel vm)
        {  
            if(!ModelState.IsValid){
                return View("SavePatient", vm);
            }
            
            SavePatientViewModel patientVm = await _patientService.Add(vm);

            if(patientVm != null && patientVm.Id != 0){
                patientVm.Picture = UploadFile(vm.PictureFile, patientVm.Id);
                await _patientService.Update(patientVm, patientVm.Id);
            }

            return RedirectToRoute(new {Controller="Patient", action="Index"});
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View("SavePatient", await _patientService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePatientViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SavePatient", vm);
            }

            await _patientService.Update(vm, vm.Id);
            return RedirectToRoute(new { controller = "Patient", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _patientService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {  
            await _patientService.Delete(id);
            return RedirectToRoute(new { controller = "Patient", action = "Index" });
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imageUrl ="")
        {
            if (isEditMode && file == null)
            {
                return imageUrl;
            }

            string basePath = $"/Images/Patients/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string filename = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, filename);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            if (isEditMode)
            {
                string[] oldImagePath = imageUrl.Split('/');
                string oldImageName = oldImagePath[^1];
                string completeImageOldPath = Path.Combine(path, oldImageName);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }

            return $"{basePath}/{filename}";
        }

    }
}

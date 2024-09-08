using Microsoft.AspNetCore.Mvc;
using PatientMgmt.Core.Application;
using Microsoft.AspNetCore.Authorization;

namespace PatientMgmt.Controllers
{
    [Authorize]
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

        public IActionResult Create()
        {  
            return View("SaveDoctor", new SaveDoctorViewModel());
        }
        
        [HttpPost]
        public async Task <IActionResult> Create(SaveDoctorViewModel vm)
        {  
            if(!ModelState.IsValid){
                return View("SaveDoctor", vm);
            }
            
            SaveDoctorViewModel doctorVm = await _doctorService.Add(vm);

            if(doctorVm != null && doctorVm.Id != 0){
                doctorVm.Picture = UploadFile(vm.PictureFile, doctorVm.Id);
                await _doctorService.Update(doctorVm, doctorVm.Id);
            }

            return RedirectToRoute(new {Controller="Doctor", action="Index"});
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveDoctor", await _doctorService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveDoctorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveDoctor", vm);
            }
            /*SaveDoctorViewModel doctorVm = await _doctorService.GetByIdSaveViewModel(vm.Id);
            vm.Picture = UploadFile(vm.PictureFile, vm.Id, true, doctorVm.Picture);*/

            await _doctorService.Update(vm, vm.Id);
            return RedirectToRoute(new { controller = "Doctor", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _doctorService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            
            await _doctorService.Delete(id);
            return RedirectToRoute(new { controller = "Doctor", action = "Index" });
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imageUrl ="")
        {
            if (isEditMode && file == null)
            {
                return imageUrl;
            }

            string basePath = $"/Images/Doctors/{id}";
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

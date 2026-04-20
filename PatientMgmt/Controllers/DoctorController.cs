using Microsoft.AspNetCore.Mvc;
using PatientMgmt.Core.Application;
using Microsoft.AspNetCore.Authorization;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;

namespace PatientMgmt.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(IDoctorService doctorService, BlobServiceClient blobServiceClient, ILogger<DoctorController> logger)
        {
            _doctorService = doctorService;
            _blobServiceClient = blobServiceClient;
            _logger = logger;
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
        public async Task<IActionResult> Create(SaveDoctorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveDoctor", vm);
            }

            // 1. Guardamos el doctor en la base de datos primero para tener su ID
            SaveDoctorViewModel doctorVm = await _doctorService.Add(vm);

            if (doctorVm != null && doctorVm.Id != 0 && vm.PictureFile != null)
            {
                var container = _blobServiceClient.GetBlobContainerClient("doctorpictures");
                // Ensure container exists before uploading
                await container.CreateIfNotExistsAsync(PublicAccessType.Blob);

                // El nombre del archivo en Azure debe ser único
                // Ejemplo: doctor-1.jpg o un Guid
                string fileName = $"doctor-{doctorVm.Id}{Path.GetExtension(vm.PictureFile.FileName)}";
                var blobClient = container.GetBlobClient(fileName);

                using (Stream stream = vm.PictureFile.OpenReadStream())
                {
                    // Subimos directamente el stream sin leerlo antes
                    await blobClient.UploadAsync(stream, true);
                }

                // 2. Guardamos la URL resultante en el objeto para actualizar la BD
                doctorVm.Picture = blobClient.Uri.ToString();
                await _doctorService.Update(doctorVm, doctorVm.Id);
            }

            return RedirectToRoute(new { Controller = "Doctor", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveDoctor", await _doctorService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveDoctorViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("SaveDoctor", vm);
                }

                await _doctorService.Update(vm, vm.Id);
                return RedirectToRoute(new { controller = "Doctor", action = "Index" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en Edit Doctor {DoctorId}", vm.Id);
                ModelState.AddModelError(string.Empty, "Ocurrió un error al editar el doctor.");
                return View("SaveDoctor", vm);
            }
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
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error escribiendo fichero local para doctor {DoctorId}", id);
                throw;
            }
        }
    }
}

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using PetStoreProject.Logic;
using PetStoreProject.Models;

namespace PetStoreProject.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IRepository _repo;
        private IWebHostEnvironment _webHost;
        public AdministratorController(IRepository repo, IWebHostEnvironment webHost)
        {
            _repo = repo;
            _webHost = webHost;
        }
        public IActionResult Index(int? categoryId)
        {
            IEnumerable<Animal> animalCatalog;

            if (categoryId.HasValue && categoryId > 0)
            {
                // Get animals for the selected category
                animalCatalog = _repo.GetAnimalsByCategory(categoryId.Value);
            }
            else
            {
                // Get all animals if no category is selected
                animalCatalog = _repo.GetAllAnimals();
            }

            ViewBag.Categories = _repo.GetAllCategories();
            ViewBag.SelectedCategory = categoryId ?? 0; // Pass the selected category for the dropdown
            return View(animalCatalog);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _repo.GetAllCategories();
            var animal = _repo.GetAnimalById(id);
            return View(animal);
        }
        public IActionResult ConfirmDelete(int animalId)
        {
            return View(animalId);
        }
        public IActionResult RemoveAnimal(int animalId)
        {
            _repo.RemoveAnimal(animalId);
            return RedirectToAction("Index", "Administrator");
        }
       
        public IActionResult EditAnimal(Animal animal, IFormFile?SelectedPicture)
        {
            if (SelectedPicture != null)
            {
                string uploadFile = Path.Combine(_webHost.WebRootPath, "Image");
                string fileName = Path.GetFileName(SelectedPicture.FileName);
                string fileSavePath = Path.Combine(uploadFile, fileName);

                using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
                {
                    SelectedPicture.CopyTo(stream);
                }
                             animal.PictureName = $"/Image/{fileName}";
            }
            _repo.UpdateAnimal(animal);
            return RedirectToAction("Index", "Administrator");
        }
        
        public IActionResult AddAnimal(Animal animal, IFormFile? SelectedPicture)
        {
            string uploadFile = Path.Combine(_webHost.WebRootPath, "Image");
            string fileName = Path.GetFileName(SelectedPicture.FileName);
            string fileSavePath = Path.Combine(uploadFile, fileName);

            using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
            {
                SelectedPicture.CopyTo(stream);
            }
            animal.PictureName = $"/Image/{fileName}";
            _repo.AddAnimal(animal);
            return RedirectToAction("Index", "Administrator");
        }
        public IActionResult AddAnimalView(int? categoryId)
        {
            IEnumerable<Animal> animalCatalog;

            if (categoryId.HasValue && categoryId > 0)
            {
                animalCatalog = _repo.GetAnimalsByCategory(categoryId.Value);
            }
            else
            {               
                animalCatalog = _repo.GetAllAnimals();
            }

            ViewBag.Categories = _repo.GetAllCategories();
            ViewBag.SelectedCategory = categoryId ?? 0;
            return View();
        }
    }
}

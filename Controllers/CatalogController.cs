using Microsoft.AspNetCore.Mvc;
using PetStoreProject.Logic;
using PetStoreProject.Models;

namespace PetStoreProject.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IRepository _repo;

        public CatalogController(IRepository repo)
        {
            _repo = repo;
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
        public IActionResult AnimalDetails(int animalId)
        {
            var animal = _repo.GetAnimalById(animalId);
            return View(animal);
        }
        public IActionResult AddComment(int animalId, string content)
        {
            _repo.AddComment(animalId, content);
            return RedirectToAction("AnimalDetails", new { animalId = animalId });
        }

    }
}

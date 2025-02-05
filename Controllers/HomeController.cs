using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetStoreProject.Context;
using PetStoreProject.Logic;
using PetStoreProject.Service;

namespace PetStoreProject.Controllers
{
    public class HomeController : Controller
    {

        private readonly IRepository _repo;

        public HomeController(IRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var animals = _repo.GetMostPopularAnimals();
            return View(animals);
        }

      
       

    }
}

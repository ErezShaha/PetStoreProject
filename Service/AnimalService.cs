using Microsoft.EntityFrameworkCore;
using PetStoreProject.Context;
using PetStoreProject.Logic;
using PetStoreProject.Models;

namespace PetStoreProject.Service
{
    public class AnimalService : IRepository
    {
        private readonly PetStoreContext _context;
        public AnimalService(PetStoreContext context)
        {
            _context = context;
        }
        public void AddAnimal(Animal animal)
        {
            _context.Animals.Add(animal);
            _context.SaveChanges();
        }

        public void AddComment(int animalId, string content)
        {
            var comment = new Comment { AnimalId = animalId, Content = content };
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public void UpdateAnimal(Animal Editedanimal)
        {
            _context.Animals.Update(Editedanimal);
            _context.SaveChanges();
        }

        public IQueryable<Animal> GetAllAnimals()
        {
            return _context.Animals.Where(a => a.IsRelevant);
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Animal GetAnimalById(int id)
        {
            var animal = _context.Animals.Include(a => a.Category).Include(a => a.Comments).SingleOrDefault(a => a.AnimalId == id);
            return animal!;
        }

        public List<Animal> GetAnimalsByCategory(int categoryId)
        {
            var animalsByCategory = _context.Animals.Where(a => a.CategoryId == categoryId).Where(a => a.IsRelevant).ToList();
            return animalsByCategory;
        }

        public IQueryable<Animal> GetMostPopularAnimals()
        {
            var popularAnimals = _context.Animals.Where(a => a.IsRelevant).OrderByDescending(a => a.Comments.Count).Take(2).Include(a => a.Comments);
            return popularAnimals;
        }

        public void RemoveAnimal(int id)
        {
            var animal = _context.Animals.FirstOrDefault(a => a.AnimalId == id);
            animal.IsRelevant = false;
            _context.Animals.Update(animal);
            _context.SaveChanges();

        }
    }
}

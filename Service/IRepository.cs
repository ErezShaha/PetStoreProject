using PetStoreProject.Models;

namespace PetStoreProject.Logic
{
    public interface IRepository
    {
        IQueryable<Animal> GetAllAnimals();
        IQueryable<Animal> GetMostPopularAnimals();
        List<Animal> GetAnimalsByCategory(int categoryId);
        List<Category> GetAllCategories();
        Animal GetAnimalById(int id);
        void AddAnimal(Animal animal);
        void RemoveAnimal(int id);
        void UpdateAnimal(Animal animal);
        void AddComment(int animalId, string content);
    }
}

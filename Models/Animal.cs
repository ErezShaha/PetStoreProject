namespace PetStoreProject.Models
{
    public class Animal
    {
        public int AnimalId { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Description { get; set; }
        public string? PictureName { get; set; }
        public bool IsRelevant { get; set; } = true;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<Comment>? Comments { get; set; } = new List<Comment>();
    }
}

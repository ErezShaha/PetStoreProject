using System.ComponentModel.DataAnnotations;

namespace PetStoreProject.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int AnimalId { get; set; }
        public Animal? Animal { get; set; }
        public string? Content { get; set; }
    }
}

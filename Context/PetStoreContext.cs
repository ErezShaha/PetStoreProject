using Microsoft.EntityFrameworkCore;
using PetStoreProject.Models;

namespace PetStoreProject.Context
{
    public class PetStoreContext : DbContext
    {
        public PetStoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Comment>().HasOne(c => c.Animal).WithMany(a => a.Comments)
                .HasForeignKey(c => c.AnimalId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>().HasData(
                new Category {CategoryId = 1, Name = "Birds"},
                new Category {CategoryId = 2, Name = "Mammals"},
                new Category {CategoryId = 3, Name = "Reptiles"},
                new Category {CategoryId = 4, Name = "Fish"}
                );

            modelBuilder.Entity<Animal>().HasData(
                new Animal {AnimalId = 1, Name = "Tukikoo", Age = 3, Description = "A very cute and dumb but also smart parrot", CategoryId = 1, PictureName = "/Image/IMG-20241101-WA0040.jpg"},
                new Animal {AnimalId = 2, Name = "Grizzly Bear", Age = 22, Description = "Grizzly bear one of natures most funny and dangerous creations", CategoryId = 2, PictureName = "/Image/bearbear.jpg" },
                new Animal {AnimalId = 3, Name = "Crocodile", Age = 92, Description = "A modern dinosaur powerful as it is indifferent", CategoryId = 3, PictureName = "/Image/Crocodile.jpg" },
                new Animal {AnimalId = 4, Name = "Mojombo", Age = 123, Description = "A gaint goldfish...", CategoryId = 4, PictureName = "/Image/goldfish.jpg" }
                );

            modelBuilder.Entity<Comment>().HasData(
                new Comment {AnimalId = 1, CommentId = 1, Content = "A very cute parrot, enjoys eating apples, pears and plums."},
                new Comment {AnimalId = 1, CommentId = 2, Content = "Bites a lot but still likes it when you scratch the back of it's head."},
                new Comment {AnimalId = 1, CommentId = 3, Content = "10/10 best parrot." },
                new Comment {AnimalId = 2, CommentId = 4, Content = "I'm not sure if a grizzly is a good idea for a pet but it is definetly cute." },
                new Comment {AnimalId = 3, CommentId = 5, Content = "very chill doesn't do to much. good for a chill home if you don't want to waste all your time draining your pets energy." },
                new Comment {AnimalId = 3, CommentId = 6, Content = "I'm a proud owner of two crocodiles! miguel and sasha. they're both cute and lovable therapy animals fit for any home or any pool. if you own a golf course it can be an excellent home for them!"},
                new Comment {AnimalId = 4, CommentId = 7, Content = "just a gaint goldfish.. pretty surreal."},
                new Comment {AnimalId = 4, CommentId = 8, Content = "looks tasty" },
                new Comment {AnimalId = 4, CommentId = 9, Content = "WTF" },
                new Comment {AnimalId = 1,CommentId = 10,Content = "greencheeck cunres are my favorite kind of parrots!" }
                );
        }
    }
}

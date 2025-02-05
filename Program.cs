using Microsoft.EntityFrameworkCore;
using PetStoreProject.Context;
using PetStoreProject.Logic;
using PetStoreProject.Models;
using PetStoreProject.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PetStoreContext>((options => options.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=PetStoreProjDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepository,AnimalService>();

var app = builder.Build();

using (var scoped = app.Services.CreateScope())
{
    var ctx = scoped.ServiceProvider.GetRequiredService<PetStoreContext>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
}


app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using GalaxyMapDotnet.DLL;
using GalaxyMapDotnet.DLL.Serivces;
using GalaxyMapDotnet.DLL.Serivces.Interfaces;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MapContext>(options =>
                options.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = GalaxyMap; Trusted_Connection = True; MultipleActiveResultSets = true"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMapService, MapService>();

//builder.Services.AddDbContext<MapContext>(options =>
//                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<MapContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


//TODO:
//1. Bases colonies distinction - possibly dots
//2. Regions
//3. Quadrants and circle galaxy
//4. tos and tng s1 add
//5. major powers add
//6. Onclick info
//7. 200 points add
//8. Colouring possibly?
//9. Improve scrolling 
//10. 
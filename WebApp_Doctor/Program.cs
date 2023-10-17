using Microsoft.EntityFrameworkCore;
using WebApp_Doctor.Data;

var builder = WebApplication.CreateBuilder(args);

/*builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddHttpClient();
builder.Services.AddHttpClient("RestfulApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5179"); // Replace with the actual base URL of your Restful API
});*/


// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();

// Add services to the container.(for .5 different from .6)
/*builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();
*/
var app = builder.Build();

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
    name: "webapp_viewpatient",
    pattern: "WebApp_ViewPatient/ViewPatientLists",
    defaults: new { controller = "WebApp_ViewPatient", action = "ViewPatientLists" }
);

app.Run();

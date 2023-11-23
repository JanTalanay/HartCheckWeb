using System.Security.Claims;
using HartCheck_Doctor_test.Data;
using HartCheck_Doctor_test.FileUploadService;
using HartCheck_Doctor_test.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddScoped<IFileUploadService, LocalFileUploadService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Doctor", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "2");
    });
    
});
builder.Services.AddDbContext<datacontext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


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
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "ChangePassword",
        pattern: "Account/ChangePassword/{hash}",
        defaults: new { controller = "Account", action = "ChangePassword" }
    );
    endpoints.MapControllerRoute(
        name: "AddMedicine",
        pattern: "Patient/ViewPatient/{patientID}",
        defaults: new { controller = "Patient", action = "ViewPatient" }
    );
    endpoints.MapControllerRoute(
        name: "AddMedicine",
        pattern: "Patient/AddMedicine/{patientID}",
        defaults: new { controller = "Patient", action = "AddMedicine" }
    );
    endpoints.MapControllerRoute(
        name: "AddDiagnosis",
        pattern: "Patient/AddDiagnosis/{patientID}",
        defaults: new { controller = "Patient", action = "AddDiagnosis" }
    );
    endpoints.MapControllerRoute(
        name: "AddCondition",
        pattern: "Patient/AddCondition/{patientID}",
        defaults: new { controller = "Patient", action = "AddCondition" }
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Login}/{id?}"
    );
});

app.MapHub<ChatHub>("/chatHub");

app.Run();

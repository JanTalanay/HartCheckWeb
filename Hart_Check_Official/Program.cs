using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBugReportRepository, BugReportRepository>();
builder.Services.AddScoped<IBodyMassRepository, BodyMassRepository>();
builder.Services.AddScoped<IMedicalConditionRepository, MedicalConditionRepository>();
builder.Services.AddScoped<IPreviousMedRepository, PreviousMedRepository>();
builder.Services.AddScoped<IMedicalHistoryRepository, MedicalHistoryRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IBloodPressureRepository, BloodPressureRepository>();
builder.Services.AddScoped<IBMITypeRepository, BMITypeRepository>();
builder.Services.AddScoped<IConsultationRepository, ConsultationRepository>();

builder.Services.AddScoped<IViewPatientListsRepository, ViewPatientListsRepository>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<datacontext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

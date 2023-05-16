using AppDrones.Core.Data;
using AppDrones.Core.Dto;
using AppDrones.Core.Extensions;
using AppDrones.Core.Interfaces;
using AppDrones.Core.Models;
using AppDrones.Core.Services;
using AppDrones.Core.Validations;
using AppDrones.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddTransient<IDrone, DroneService>();
builder.Services.AddScoped<IValidator<RegistryReqDto>, RegistryDroneValidator>();
builder.Services.AddScoped<IValidator<IEnumerable<LoadMedicationReqDto>>, LoadingMedicationValidator>();

//Add cors support
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Generate random data if database is empty
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<DatabaseContext>();

    if (!dbContext.Drone.Any())
    {
        var drones = DataGenerator.GenerateDrones(10);

        var random = new Random();

        foreach (var drone in drones)
        {
            // Asignar medicamentos aleatorios
            if (random.Next(2) == 0)
            {
                var medications = DataGenerator.GenerateMedications(random.Next(1, 6), drones);
                drone.Medications = new List<Medication>();
                drone.Medications = medications;
            }

            // Assign random state
            // drone.State = random.NextEnum<State>();

            // Assign random battery level
            // drone.BatteryCapacity = random.Next(0, 100);

            dbContext.Drone.Add(drone);
        }
        dbContext.SaveChanges();
    }
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("MyPolicy");

app.Run();

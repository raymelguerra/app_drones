using AppDrones.Core.Dto;
using AppDrones.Core.Interfaces;
using AppDrones.Core.Services;
using AppDrones.Core.Validations;
using AppDrones.Data;
using FluentValidation;

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("MyPolicy");

app.Run();

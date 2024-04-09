using Microsoft.EntityFrameworkCore;
using SolarPowerAPI.Data;
using SolarPowerAPI.Mock;
using SolarPowerAPI.Models;
using SolarPowerAPI.Repositories;
using SolarPowerAPI.Repositories.QueryBuilders;
using SolarPowerPlantAPI.Mapping;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<SolarPlantsMock>();
builder.Services.AddTransient<ProductionMock>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var settings = builder.Configuration.GetSection("Settings").Get<Settings>();
builder.Services.AddDbContext<SolarPowerContext>(opt => opt.UseSqlServer(settings?.ConnectionString));

builder.Services.AddScoped<ISolarPlantRepo, SolarPlantRepo>();
builder.Services.AddScoped<IProductionRepo, ProductionRepo>();
builder.Services.AddScoped<IProductionQueryBuilder, ProductionQueryBuilder>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

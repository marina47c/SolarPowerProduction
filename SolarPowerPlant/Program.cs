using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using SolarPowerAPI.Data;
using SolarPowerAPI.Mock;
using SolarPowerAPI.Models;
using SolarPowerAPI.Repositories;
using SolarPowerAPI.Repositories.QueryBuilders;
using SolarPowerPlantAPI.Mapping;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<SolarPlantsMock>();
builder.Services.AddTransient<ProductionMock>();
builder.Services.AddTransient<RolesMock>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Solar Power API",
        Version = "v1",
    });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

Settings? settings = builder.Configuration.GetSection("Settings").Get<Settings>();
builder.Services.AddDbContext<SolarPowerContext>(opt => opt.UseSqlServer(settings?.ConnectionStrings?.SolarPowerConnectionString));
builder.Services.AddDbContext<SolarPowerAuthContext>(opt => opt.UseSqlServer(settings?.ConnectionStrings?.SolarPowerAuthConnectionString));

builder.Services.AddScoped<ISolarPlantRepo, SolarPlantRepo>();
builder.Services.AddScoped<IProductionRepo, ProductionRepo>();
builder.Services.AddScoped<IProductionQueryBuilder, ProductionQueryBuilder>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("SolarPower")
    .AddEntityFrameworkStores<SolarPowerAuthContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1; 
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = settings?.Jwt?.Issuer ?? String.Empty,
        ValidAudience = settings?.Jwt?.Audience ?? String.Empty,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings?.Jwt?.Key ?? String.Empty))
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using ToTienDung0300567.DbContexts;
using ToTienDung0300567.Services.Implements;
using ToTienDung0300567.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Set environment to Development if not specified
if (string.IsNullOrEmpty(builder.Environment.EnvironmentName))
{
    builder.Environment.EnvironmentName = Environments.Development;
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Enhanced Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ToTienDung0300567 API",
        Version = "v1",
        Description = "Test-Exam",
        
    });
    
    // Enable XML comments if you have them
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
    
    // Add operation filter to include action descriptions
    c.EnableAnnotations();
});

builder.Services.AddDbContext<AppDbContexts0300567De21026>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IMechanicService0300567De21044, MechanicService0300567De21055>();

var app = builder.Build();

// Enable Swagger in all environments
app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToTienDung0300567 API v1");
    c.RoutePrefix = "swagger";
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
    c.DefaultModelsExpandDepth(0); // Hide models by default
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Add a redirect from root to Swagger UI
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
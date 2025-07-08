using Microsoft.EntityFrameworkCore;
using MasiniAPI.Data;
using MasiniAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
    "Server=localhost;Database=masini_db;Uid=root;Pwd=12112003Ioan;";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "http://localhost:5174", "http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        context.Database.EnsureCreated();
        Console.WriteLine("Database created successfully!");
        
        // Seed data if database is empty
        if (!context.Masini.Any())
        {
            var sampleMasini = new List<Masina>
            {
                new Masina { Marca = "Toyota", Model = "Corolla", An = 2020, Motor = "1.6L" },
                new Masina { Marca = "BMW", Model = "X5", An = 2022, Motor = "3.0L" },
                new Masina { Marca = "Audi", Model = "A4", An = 2021, Motor = "2.0L" },
                new Masina { Marca = "Mercedes", Model = "C-Class", An = 2023, Motor = "2.0L" },
                new Masina { Marca = "Volkswagen", Model = "Golf", An = 2019, Motor = "1.4L" }
            };
            
            context.Masini.AddRange(sampleMasini);
            await context.SaveChangesAsync();
            Console.WriteLine("Sample data added successfully!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error creating database: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowVueApp");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

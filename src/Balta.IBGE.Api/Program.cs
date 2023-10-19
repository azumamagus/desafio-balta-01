using Balta.IBGE.Infra;
using Balta.IBGE.Infra.Database;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<IBGEDbContext>(options =>
{
    options.UseSqlServer(connectionString,
        b => b.MigrationsAssembly(typeof(InfraAssemblyReference).Assembly.ToString()));
    
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging(true);
    options.LogTo(action: Console.WriteLine,
                  categories: new[]
                  {
                  DbLoggerCategory.Database.Command.Name
                  },
                  minimumLevel: LogLevel.Information);
});

var app = builder.Build();

//app.MapPost("/cities", async (City city, IbgeDbContext db) =>
//{
//    db.Cities.Add(city);
//    await db.SaveChangesAsync();

//    return Results.Created($"/cities/{city.Id}", city);
//});

//app.MapGet("/cities", async (IbgeDbContext db) => await db.Cities.ToListAsync());

//app.MapGet("/cities/{id:int}", async (int id, IbgeDbContext db) =>
//{
//    return await db.Cities.FindAsync(id)
//                is City city
//                ? Results.Ok(city)
//                : Results.NotFound();
//});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using IServiceScope serviceScope = app.Services.CreateScope();
using IBGEDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<IBGEDbContext>();
dbContext.Database.Migrate();

app.Run();

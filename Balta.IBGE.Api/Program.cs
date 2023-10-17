using Balta.IBGE.Api.Data;
using Balta.IBGE.Api.Models;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<IbgeContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

app.MapPost("/cities", async (City city, IbgeContext db) =>
{
    db.Cities.Add(city);
    await db.SaveChangesAsync();

    return Results.CreatedAtRoute($"/cities/{city.Id}");
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

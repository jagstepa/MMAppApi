using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMAppApi.Interfaces;
using MMAppApi.Models;
using MMAppApi.Repository;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

#region Database Configure
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MmappContext>(options =>
    options.UseSqlServer(connectionString));

#endregion

builder.Services.ConfigureAll<JsonOptions>(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

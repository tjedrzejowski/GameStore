using FluentValidation;
using GameStore.Api.Contracts;
using GameStore.Api.Data;
using GameStore.Api.Services;
using GameStore.Api.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNpgsql<GameStoreContext>(connectionString);

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGenreService, GenreService>();

builder.Services.AddTransient<IValidator<CreateGameDto>, CreateGameDtoValidator>();
builder.Services.AddTransient<IValidator<UpdateGameDto>, UpdateGameDtoValidator>();

var app = builder.Build();

// Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
await app.MigrateDatabaseAsync();
app.Run();
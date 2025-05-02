using FluentValidation;
using GameStore.Api.Contracts;
using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.Services;
using GameStore.Api.Validators;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connectionString);
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddTransient<IValidator<CreateGameDto>, CreateGameDtoValidator>();
builder.Services.AddTransient<IValidator<UpdateGameDto>, UpdateGameDtoValidator>();

var app = builder.Build();

app.MapGamesEndpoints();
app.MapGenresEndpoints();
await app.MigrateDatabaseAsync();
app.Run();
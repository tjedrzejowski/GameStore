using FluentValidation;
using GameStore.Api.Contracts;
using GameStore.Api.Services;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games");

        group.MapGet("/", async (IGameService service) =>
        {
            var games = await service.GetAllAsync();
            return Results.Ok(games);
        });

        group.MapGet("/{id}", async (int id, IGameService service) =>
        {
            var game = await service.GetByIdAsync(id);
            return game is not null ? Results.Ok(game) : Results.NotFound();
        })
        .WithName(GetGameEndpointName);

        group.MapPost("/", async (CreateGameDto newGame, IGameService service, IValidator<CreateGameDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(newGame);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var game = await service.CreateAsync(newGame);
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, IGameService service, IValidator<UpdateGameDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(updatedGame);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var uppdateSucceeded = await service.UpdateAsync(id, updatedGame);
            return uppdateSucceeded ? Results.NoContent() : Results.NotFound();
        });

        group.MapDelete("/{id}", async (int id, IGameService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        });

        return group;
    }
}
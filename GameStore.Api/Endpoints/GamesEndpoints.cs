using FluentValidation;
using GameStore.Api.Contracts;
using GameStore.Api.Data;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games");

        // crud read
        group.MapGet("/", async (GameStoreContext databaseContext) =>
        {
            var games = await databaseContext.Games
                .Include(item => item.Genre)
                .Select(item => item.ToGameSummaryDto())
                .AsNoTracking()
                .ToListAsync();

            return Results.Ok(games);
        });

        group.MapGet("/{id}", async (int id, GameStoreContext databaseContext) =>
        {
            Game? game = await databaseContext.Games.FindAsync(id);
            return game is not null ? Results.Ok(game.ToGameDetailsDto()) : Results.NotFound();
        })
        .WithName(GetGameEndpointName);

        // crud create
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext databaseContext, IValidator<CreateGameDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(newGame);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            Game game = newGame.ToEntity();
            game.Genre = databaseContext.Genres.Find(newGame.GenreId);

            databaseContext.Games.Add(game);
            await databaseContext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameSummaryDto());
        });

        //crud update
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext databaseContext, IValidator<UpdateGameDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(updatedGame);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var game = await databaseContext.Games.FindAsync(id);
            if (game is null)
            {
                // this is one way of returning result when failed to found resource
                // second option is to create new resource with given data and put it under this id
                // REST dont define what is better behaviour in that case - both are available.
                return Results.NotFound();
            }

            databaseContext.Entry(game)
                .CurrentValues
                .SetValues(updatedGame.ToEntity(id));
            await databaseContext.SaveChangesAsync();

            return Results.NoContent();
        });

        //crud delete
        group.MapDelete("/{id}", async (int id, GameStoreContext databaseContext) =>
        {
            await databaseContext.Games
                .Where(item => item.Id == id)
                .ExecuteDeleteAsync();
            return Results.NoContent();
        });

        return group;
    }
}
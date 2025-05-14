using GameStore.Api.Contracts;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto gameDto) => new()
    {
        Title = gameDto.Title,
        GenreId = gameDto.GenreId,
        Price = gameDto.Price,
        ReleaseDate = gameDto.ReleaseDate.ToUniversalTime()
    };

    public static Game ToEntity(this UpdateGameDto gameDto, int id) => new()
    {
        Id = id,
        Title = gameDto.Title,
        GenreId = gameDto.GenreId,
        Price = gameDto.Price,
        ReleaseDate = gameDto.ReleaseDate.ToUniversalTime()
    };

    public static GameSummaryDto ToGameSummaryDto(this Game game) => new(
        game.Id,
        game.Title,
        game.Genre!.Name,
        game.ReleaseDate,
        game.Price
    );

    public static GameDetailsDto ToGameDetailsDto(this Game game) => new(
        game.Id,
        game.Title,
        game.GenreId,
        game.ReleaseDate,
        game.Price
    );
}
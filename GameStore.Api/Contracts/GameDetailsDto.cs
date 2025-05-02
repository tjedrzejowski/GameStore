namespace GameStore.Api.Contracts;

public record class GameDetailsDto(
    int Id,
    string Title,
    int GenreId,
    DateTime ReleaseDate,
    decimal Price
);
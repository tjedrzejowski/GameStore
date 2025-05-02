namespace GameStore.Api.Contracts;

public record CreateGameDto(
    string Title,
    int GenreId,
    DateTime ReleaseDate,
    decimal Price
);
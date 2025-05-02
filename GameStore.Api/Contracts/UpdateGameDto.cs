namespace GameStore.Api.Contracts;

public record UpdateGameDto(
    string Title,
    int GenreId,
    DateTime ReleaseDate,
    decimal Price
);
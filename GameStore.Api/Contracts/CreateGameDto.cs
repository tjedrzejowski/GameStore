namespace GameStore.Api.Contracts;

public record CreateGameDto(
    string Title,
    int GenreId,
    string Publisher,
    DateOnly ReleaseDate,
    decimal Price
);
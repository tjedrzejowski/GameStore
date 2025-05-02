namespace GameStore.Api.Contracts;

public record UpdateGameDto(
    string Title,
    int GenreId,
    string Publisher,
    DateOnly ReleaseDate,
    decimal Price
);
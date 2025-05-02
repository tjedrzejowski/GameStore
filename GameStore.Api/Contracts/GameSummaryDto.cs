namespace GameStore.Api.Contracts;

public record class GameSummaryDto(
    int Id,
    string Title,
    string Genre,
    DateOnly ReleaseDate,
    decimal Price
);
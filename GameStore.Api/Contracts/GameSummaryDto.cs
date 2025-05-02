namespace GameStore.Api.Contracts;

public record class GameSummaryDto(
    int Id,
    string Title,
    string Genre,
    DateTime ReleaseDate,
    decimal Price
);
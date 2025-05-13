using System.Threading.Tasks;
using GameStore.Web.Interfaces;
using GameStore.Web.Models;

namespace GameStore.Web.Clients;

public class GamesClient(HttpClient httpClient) : IGamesClient
{
    // tempororary before connecting with api
    private readonly Genre[] genres = new GenresClient(httpClient).GetGenres();

    private readonly List<GameSummary> games = [
        new(){Id = 1, Title = "Mock Game I", Genre = "Action", Price = 49.99M, ReleaseDate = new DateTime(1992, 7, 15)},
        new(){Id = 2, Title = "Mock Game II", Genre = "Role-Playing (RPG)", Price = 19.99M, ReleaseDate = new DateTime(2002, 8, 19)},
        new(){Id = 3, Title = "Mock Game III", Genre = "Racing", Price = 9.99M, ReleaseDate = new DateTime(2021, 4, 1)}
   ];

    public void AddGame(GameDetails newGame)
    {
        Genre? genre = GetGenreById(newGame.GenreId);

        var summary = new GameSummary
        {
            Id = games.Count() + 1,
            Title = newGame.Title,
            Genre = genre!.Name,
            Price = newGame.Price,
            ReleaseDate = newGame.ReleaseDate
        };

        games.Add(summary);
    }

    public void UpdateGame(GameDetails updatedGame)
    {
        var existingGame = GetGameSummaryById(updatedGame.Id);

        existingGame.Title = updatedGame.Title;
        existingGame.Genre = GetGenreById(updatedGame.GenreId)!.Name;
        existingGame.Price = updatedGame.Price;
        existingGame.ReleaseDate = updatedGame.ReleaseDate;
    }

    public async Task<GameSummary[]> GetGamesAsync() => await httpClient.GetFromJsonAsync<GameSummary[]>("games") ?? Array.Empty<GameSummary>();

    public GameDetails GetGame(int id)
    {
        var game = GetGameSummaryById(id);
        var genre = genres.Single(item => string.Equals(item.Name, game.Genre, StringComparison.OrdinalIgnoreCase));

        return new GameDetails()
        {
            Id = game.Id,
            Title = game.Title,
            GenreId = genre.Id.ToString(),
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public void DeleteGame(int id)
    {
        var game = GetGameSummaryById(id);
        games.Remove(game);
    }

    private GameSummary GetGameSummaryById(int id)
    {
        var game = games.Find(item => item.Id == id);
        ArgumentNullException.ThrowIfNull(game);
        return game;
    }

    private Genre? GetGenreById(string? id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        var genre = genres.SingleOrDefault(item => item.Id == int.Parse(id));
        ArgumentNullException.ThrowIfNull(genre);
        return genre;
    }
}
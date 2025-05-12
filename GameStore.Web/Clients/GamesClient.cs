using GameStore.Web.Interfaces;
using GameStore.Web.Models;

namespace GameStore.Web.Clients;

public class GamesClient : IGamesClient
{
    // tempororary before connecting with api
    private readonly Genre[] genres = new GenresClient().GetGenres();

    private readonly List<GameSummary> games = [
        new(){Id = 1, Name = "Mock Game I", Genre = "Action", Price = 49.99M, ReleaseDate = new DateTime(1992, 7, 15)},
        new(){Id = 2, Name = "Mock Game II", Genre = "Role-Playing (RPG)", Price = 19.99M, ReleaseDate = new DateTime(2002, 8, 19)},
        new(){Id = 3, Name = "Mock Game III", Genre = "Racing", Price = 9.99M, ReleaseDate = new DateTime(2021, 4, 1)}
   ];

    public GameSummary[] GetGames() => games.ToArray();

    public void AddGame(GameDetails game)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(game.GenreId);

        var genre = genres.SingleOrDefault(item => item.Id == int.Parse(game.GenreId));
        ArgumentNullException.ThrowIfNull(genre);

        var summary = new GameSummary
        {
            Id = games.Count() + 1,
            Name = game.Name,
            Genre = genre.Name,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };

        games.Add(summary);
    }

    public GameDetails GetGame(int id)
    {
        var game = games.Find(item => item.Id == id);
        ArgumentNullException.ThrowIfNull(game);
        var genre = genres.Single(item => string.Equals(item.Name, game.Genre, StringComparison.OrdinalIgnoreCase));

        return new GameDetails()
        {
            Id = game.Id,
            Name = game.Name,
            GenreId = genre.Id.ToString(),
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }
}
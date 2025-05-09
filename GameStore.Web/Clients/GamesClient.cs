using GameStore.Web.Models;

namespace GameStore.Web.Clients;

public class GamesClient
{
    private readonly Genre[] genres = new GenresClient().GetGenres();

    private readonly List<GameSummary> games = [
        new(){Id = 1, Name = "Mock Game I", Genre = "Fighting", Price = 49.99M, ReleaseDate = new DateTime(1992, 7, 15)},
        new(){Id = 1, Name = "Mock Game II", Genre = "RPG", Price = 19.99M, ReleaseDate = new DateTime(2002, 8, 19)},
        new(){Id = 1, Name = "Mock Game III", Genre = "Simulator", Price = 9.99M, ReleaseDate = new DateTime(2021, 4, 1)}
   ];

    public GameSummary[] GetGames() => games.ToArray();

    public void AddGame(GameDetails game)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(game.GenreId);

        var summary = new GameSummary
        {
            Id = games.Count() + 1,
            Name = game.Name,
            Genre = genres.Single(item => item.Id == int.Parse(game.GenreId)).Name,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }
}
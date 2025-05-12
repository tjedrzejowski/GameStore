using GameStore.Web.Clients;
using GameStore.Web.Models;
using Microsoft.AspNetCore.Components;

namespace GameStore.Web.Components.Pages;

public partial class EditGame : ComponentBase
{
    private Genre[]? _genres;
    private readonly GenresClient _genresClient = new();
    private readonly GamesClient _gamesClient = new();

    private GameDetails _game = new()
    {
        Name = string.Empty,
        ReleaseDate = DateTime.UtcNow
    };

    protected override void OnInitialized()
    {
        _genres = _genresClient.GetGenres();
    }

    private void HandleSubmit()
    {
        _gamesClient.AddGame(_game);
    }
}
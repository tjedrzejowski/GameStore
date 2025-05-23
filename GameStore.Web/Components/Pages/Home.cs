using GameStore.Web.Interfaces;
using GameStore.Web.Models;
using Microsoft.AspNetCore.Components;

namespace GameStore.Web.Components.Pages;

public partial class Home : ComponentBase
{
    [Inject] public IGamesClient Client { get; set; } = default!;
    private GameSummary[]? games;

    protected override async Task OnInitializedAsync()
    {
        games = await Client.GetGamesAsync();
    }

    private static string GameUrl(int id) => $"/editgame/{id}";

    private string GetDeleteModalId(GameSummary game)
    {
        return $"#{DeleteGame.GetModalId(game)}";
    }
}
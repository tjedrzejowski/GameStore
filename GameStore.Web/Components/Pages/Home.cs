using GameStore.Web.Clients;
using GameStore.Web.Models;
using Microsoft.AspNetCore.Components;

namespace GameStore.Web.Components.Pages;

public partial class Home : ComponentBase
{
    private GamesClient client = new();
    private GameSummary[]? games;

    protected override void OnInitialized()
    {
        games = client.GetGames();
    }
}
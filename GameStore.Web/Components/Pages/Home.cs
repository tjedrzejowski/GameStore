using GameStore.Web.Interfaces;
using GameStore.Web.Models;
using Microsoft.AspNetCore.Components;

namespace GameStore.Web.Components.Pages;

public partial class Home : ComponentBase
{
    [Inject] public IGamesClient Client { get; set; } = default!;
    private GameSummary[]? games;

    protected override void OnInitialized()
    {
        games = Client.GetGames();
    }
}
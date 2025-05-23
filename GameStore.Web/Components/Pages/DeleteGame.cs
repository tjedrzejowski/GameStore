using System.Threading.Tasks;
using GameStore.Web.Interfaces;
using GameStore.Web.Models;
using Microsoft.AspNetCore.Components;

namespace GameStore.Web.Components.Pages;

public partial class DeleteGame : ComponentBase
{
    [Parameter] public GameSummary? Game { get; set; }

    [Inject] public NavigationManager NavigationManager { get; private set; } = default!;
    [Inject] public IGamesClient GamesClient { get; private set; } = default!;

    private string _title = string.Empty;

    protected override void OnParametersSet()
    {
        _title = $"Delete {Game!.Title}";
    }

    public static string GetModalId(GameSummary? gameSummary)
    {
        ArgumentNullException.ThrowIfNull(gameSummary);
        return $"deleteModal:{gameSummary.Id}";
    }

    private async Task ConfirmAsync()
    {
        await GamesClient.DeleteGameAsync(Game!.Id);
        NavigationManager.Refresh();
    }
}
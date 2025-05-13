using System.Linq.Expressions;
using System.Threading.Tasks;
using GameStore.Web.Interfaces;
using GameStore.Web.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace GameStore.Web.Components.Pages;

public partial class EditGame : ComponentBase
{
    private const string ROOT = "/";

    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IGenresClient GenresClient { get; set; } = default!;
    [Inject] private IGamesClient GamesClient { get; set; } = default!;

    [Parameter]
    public int? Id { get; set; }

    [SupplyParameterFromForm]
    public GameDetails Game { get; set; } = default!;

    private Genre[]? _genres;
    private string _title = string.Empty;

    protected override async Task OnParametersSetAsync()
    {
        if (Game is not null)
        {
            return;
        }

        if (Id is not null)
        {
            Game = await GamesClient.GetGameAsync(Id.Value);
            _title = $"Edit {Game.Title}";
        }
        else
        {
            Game = new()
            {
                Title = string.Empty,
                ReleaseDate = DateTime.UtcNow
            };

            _title = $"New Game";
        }
    }

    protected override void OnInitialized()
    {
        _genres = GenresClient.GetGenres();
    }

    private async Task HandleSubmitAsync()
    {
        ArgumentNullException.ThrowIfNull(Game);

        if (Id is null)
        {
            await GamesClient.AddGameAsync(Game);
        }
        else
        {
            Game.Id = Id.Value;
            await GamesClient.UpdateGameAsync(Game);
        }
        NavigationManager.NavigateTo(ROOT);
    }

    #region Rendering
    /*
        <div class="mb-3">
            <label for="name" class="form-label">Name:</label>
            <InputText id="name" @bind-Value="_game.Name" class="form-control" />
        </div>
     */
    private RenderFragment RenderInputText(string id, string label, string value, EventCallback<string> valueChanged, Expression<Func<string>> valueExpression) =>
        builder =>
        {
            //<div class="mb-3">
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "mb-3");

            //<label for="name" class="form-label">Name:</label>
            builder.OpenElement(2, "label");
            builder.AddAttribute(3, "for", id);
            builder.AddContent(4, label);
            builder.CloseElement();

            // <InputText id="name" @bind-Value="_game.Name" class="form-control" />
            builder.OpenComponent<InputText>(5);
            builder.AddAttribute(6, "id", id);
            builder.AddAttribute(7, "class", "form-control");
            builder.AddAttribute(8, "bind-Value", value);
            builder.AddAttribute(9, "ValueChanged", valueChanged);
            builder.AddAttribute(10, "ValueExpression", valueExpression);
            builder.CloseComponent();

            // </div>
            builder.CloseElement();
        };
    #endregion
}
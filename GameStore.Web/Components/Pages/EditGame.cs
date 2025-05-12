using System.Linq.Expressions;
using GameStore.Web.Clients;
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

    [SupplyParameterFromForm]
    public GameDetails Game { get; set; } = new()
    {
        Name = string.Empty,
        ReleaseDate = DateTime.UtcNow
    };

    private Genre[]? _genres;

    protected override void OnInitialized()
    {
        _genres = GenresClient.GetGenres();
    }

    private void HandleSubmit()
    {
        GamesClient.AddGame(Game);
        NavigationManager.NavigateTo(ROOT);
    }

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
}
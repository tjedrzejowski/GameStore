using GameStore.Api.Services;

namespace GameStore.Api.Endpoints;

public static class GenresEndpoints
{
    private const string GenresGroupName = "genres";
    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(GenresGroupName);

        group.MapGet("/", async (IGenreService service) => await service.GetAll());

        return group;
    }
}
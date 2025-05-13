using GameStore.Web.Interfaces;
using GameStore.Web.Models;

namespace GameStore.Web.Clients;

public class GenresClient(HttpClient httpClient) : IGenresClient
{
    public async Task<Genre[]> GetGenresAsync() => await httpClient.GetFromJsonAsync<Genre[]>("genre") ?? Array.Empty<Genre>();
}
using GameStore.Web.Interfaces;
using GameStore.Web.Models;

namespace GameStore.Web.Clients;

public class GamesClient(HttpClient httpClient) : IGamesClient
{
    public async Task AddGameAsync(GameDetails newGame) => await httpClient.PostAsJsonAsync("games", newGame);

    public async Task UpdateGameAsync(GameDetails updatedGame) => await httpClient.PutAsJsonAsync($"games/{updatedGame.Id}", updatedGame);

    public async Task<GameSummary[]> GetGamesAsync() => await httpClient.GetFromJsonAsync<GameSummary[]>("games") ?? Array.Empty<GameSummary>();

    public async Task<GameDetails> GetGameAsync(int id) => await httpClient.GetFromJsonAsync<GameDetails>($"games/{id}") ?? throw new Exception("GameClient.GetGameAsync: Could not find the game");

    public async Task DeleteGameAsync(int id) => await httpClient.DeleteAsync($"games/{id}");
}
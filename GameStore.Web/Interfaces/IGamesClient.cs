using GameStore.Web.Models;

namespace GameStore.Web.Interfaces;

public interface IGamesClient
{
    Task AddGameAsync(GameDetails newGame);
    Task UpdateGameAsync(GameDetails updatedGame);
    Task<GameSummary[]> GetGamesAsync();
    Task<GameDetails> GetGameAsync(int id);
    Task DeleteGameAsync(int id);
}

using GameStore.Web.Models;

namespace GameStore.Web.Interfaces;

public interface IGamesClient
{
    void AddGame(GameDetails newGame);
    void UpdateGame(GameDetails pdatedGame);
    Task<GameSummary[]> GetGamesAsync();
    GameDetails GetGame(int id);
    void DeleteGame(int id);
}

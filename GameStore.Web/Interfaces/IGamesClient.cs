using GameStore.Web.Models;

namespace GameStore.Web.Interfaces;

public interface IGamesClient
{
    void AddGame(GameDetails newGame);
    void UpdateGame(GameDetails pdatedGame);
    GameSummary[] GetGames();
    GameDetails GetGame(int id);
}

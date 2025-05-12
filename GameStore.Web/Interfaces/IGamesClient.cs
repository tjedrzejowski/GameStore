using GameStore.Web.Models;

namespace GameStore.Web.Interfaces;

public interface IGamesClient
{
    void AddGame(GameDetails game);
    GameSummary[] GetGames();
}

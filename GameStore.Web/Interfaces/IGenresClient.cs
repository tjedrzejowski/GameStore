using GameStore.Web.Models;

namespace GameStore.Web.Interfaces;

public interface IGenresClient
{
    Task<Genre[]> GetGenresAsync();
}

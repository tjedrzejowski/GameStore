using GameStore.Api.Contracts;

namespace GameStore.Api.Services;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetAll();
}

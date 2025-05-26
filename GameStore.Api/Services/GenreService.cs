using GameStore.Api.Contracts;
using GameStore.Api.Data;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Services;

public class GenreService(GameStoreContext databaseContext) : IGenreService
{
    public async Task<IEnumerable<GenreDto>> GetAllAsync()
    {
        return await databaseContext.Genres
            .Select(item => item.ToDto())
            .AsNoTracking()
            .ToListAsync();
    }
}
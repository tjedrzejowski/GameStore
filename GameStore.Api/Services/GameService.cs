using GameStore.Api.Contracts;
using GameStore.Api.Data;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Services;

public class GameService(GameStoreContext databaseContext) : IGameService
{
    public async Task<IEnumerable<GameSummaryDto>> GetAllAsync()
    {
        return await databaseContext.Games
            .Include(item => item.Genre)
            .Select(item => item.ToGameSummaryDto())
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<GameDetailsDto?> GetByIdAsync(int id)
    {
        var game = await databaseContext.Games
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.Id == id);

        return game?.ToGameDetailsDto();
    }

    public async Task<GameSummaryDto> CreateAsync(CreateGameDto gameDto)
    {
        var game = gameDto.ToEntity();
        game.Genre = databaseContext.Genres.Find(gameDto.GenreId);

        databaseContext.Add(game);
        await databaseContext.SaveChangesAsync();

        return game.ToGameSummaryDto();
    }

    public async Task<bool> UpdateAsync(int id, UpdateGameDto gameDto)
    {
        var game = await databaseContext.Games.FindAsync(id);
        if (game is null)
        {
            return false;
        }

        databaseContext.Entry(game)
            .CurrentValues
            .SetValues(gameDto.ToEntity(id));
        await databaseContext.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAsync(int id)
    {
        await databaseContext.Games
            .Where(item => item.Id == id)
            .ExecuteDeleteAsync();
    }
}
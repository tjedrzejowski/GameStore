using GameStore.Api.Contracts;

namespace GameStore.Api.Services;

public interface IGameService
{
    Task<IEnumerable<GameSummaryDto>> GetAllAsync();
    Task<GameDetailsDto?> GetByIdAsync(int id);
    Task<GameSummaryDto> CreateAsync(CreateGameDto gameDto);
    Task<bool> UpdateAsync(int id, UpdateGameDto gameDto);
    Task DeleteAsync(int id);
}
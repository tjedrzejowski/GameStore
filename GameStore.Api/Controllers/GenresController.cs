using GameStore.Api.Contracts;
using GameStore.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[ApiController]
[Route("genres")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _service;

    public GenresController(IGenreService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<GenreDto>> GetGenresAsync()
    {
        return await _service.GetAllAsync();
    }
}
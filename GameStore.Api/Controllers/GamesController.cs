using FluentValidation;
using GameStore.Api.Contracts;
using GameStore.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[ApiController]
[Route("games")]
public class GamesController : ControllerBase
{
    private readonly IGameService _service;
    private readonly IValidator<CreateGameDto> _createGameValidator;
    private readonly IValidator<UpdateGameDto> _updateGameValidator;

    public GamesController(
        IGameService service,
        IValidator<CreateGameDto> createGameValidator,
        IValidator<UpdateGameDto> updateGameValidator)
    {
        _service = service;
        _createGameValidator = createGameValidator;
        _updateGameValidator = updateGameValidator;
    }

    [HttpGet]
    public async Task<IResult> GetAllAsync()
    {
        var games = await _service.GetAllAsync();
        return Results.Ok(games);
    }

    [HttpGet("{id}", Name = "GetGame")]
    public async Task<IResult> GetByIdAsync(int id)
    {
        var game = await _service.GetByIdAsync(id);
        return game is not null ? Results.Ok(game) : Results.NotFound();
    }

    [HttpPost]
    public async Task<IResult> CreateAsync(CreateGameDto newGame)
    {
        var validationResult = await _createGameValidator.ValidateAsync(newGame);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var game = await _service.CreateAsync(newGame);
        return Results.CreatedAtRoute("GetGame", new { id = game.Id }, game);
    }

    [HttpPut("{id}")]
    public async Task<IResult> UpdateAsync(int id, UpdateGameDto updatedGame)
    {
        var validationResult = await _updateGameValidator.ValidateAsync(updatedGame);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var uppdateSucceeded = await _service.UpdateAsync(id, updatedGame);
        return uppdateSucceeded ? Results.NoContent() : Results.NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteAsync(int id)
    {
        await _service.DeleteAsync(id);
        return Results.NoContent();
    }
}
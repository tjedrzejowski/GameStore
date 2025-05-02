using GameStore.Api.Contracts;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping;

public static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre) => new(genre.Id, genre.Name);

    public static Genre ToEntity(this GenreDto dto) => new()
    {
        Id = dto.Id,
        Name = dto.Name
    };
}
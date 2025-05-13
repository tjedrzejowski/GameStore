using GameStore.Web.Interfaces;
using GameStore.Web.Models;

namespace GameStore.Web.Clients;

public class GenresClient(HttpClient httpClient) : IGenresClient
{
    private readonly Genre[] genres = [

            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "Adventure" },
            new Genre { Id = 3, Name = "Role-Playing (RPG)" },
            new Genre { Id = 4, Name = "Shooter" },
            new Genre { Id = 5, Name = "Strategy" },
            new Genre { Id = 6, Name = "Simulation" },
            new Genre { Id = 7, Name = "Sports" },
            new Genre { Id = 8, Name = "Racing" },
            new Genre { Id = 9, Name = "Puzzle" },
            new Genre { Id = 10, Name = "Platformer" }
    ];

    public Genre[] GetGenres() => genres;
}
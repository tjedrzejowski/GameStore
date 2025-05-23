namespace GameStore.Api.Entities;

public class Game
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
}
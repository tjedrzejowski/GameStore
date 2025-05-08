namespace GameStore.Web.Models;

public class GameDetails
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? GenreId { get; set; }
    public decimal Price { get; set; }
    public required DateTime ReleaseDate { get; set; }
}
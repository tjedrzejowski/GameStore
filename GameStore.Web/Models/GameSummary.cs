namespace GameStore.Web.Models;

public class GameSummary
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Genre { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models;

public class GameDetails
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
    [Required] public string? GenreId { get; set; }

    [Range(0.01, 100)] public decimal Price { get; set; }
    public required DateTime ReleaseDate { get; set; }
}
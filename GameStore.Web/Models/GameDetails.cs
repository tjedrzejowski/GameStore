using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GameStore.Web.Converters;

namespace GameStore.Web.Models;

public class GameDetails
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string Title { get; set; }

    [Required(ErrorMessage = "The Genre is required.")]
    [JsonConverter(typeof(StringConverter))]
    public string? GenreId { get; set; }

    [Range(0.01, 100)] public decimal Price { get; set; }
    public required DateTime ReleaseDate { get; set; }
}
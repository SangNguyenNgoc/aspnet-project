using System.ComponentModel.DataAnnotations;
using MovieApp.Application.Feature.Movie.Dtos;

namespace MovieApp.Application.Feature.Show.Dtos;

public class CreateShowtimeRequest
{
    [Required]
    public string? CinemaId { get; set; }  // ID của rạp chiếu phim
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public List<MovieCreateShowtimeRequest> Movies { get; set; } 
}
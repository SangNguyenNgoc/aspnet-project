using System.ComponentModel.DataAnnotations;

namespace MovieApp.Application.Feature.Show.Dtos;

public class CreateShowtimeHandWork
{
    [Required]
    public string CinemaId { get; set; }  // ID của rạp chiếu phim
    [Required]
    public long HallId { get; set; }  // ID của phòng chiếu
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public string MovieId { get; set; }
    [Required]
    public long FormatId { get; set; }
}
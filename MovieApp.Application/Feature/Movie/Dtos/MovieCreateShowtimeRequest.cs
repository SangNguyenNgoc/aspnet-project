using System.ComponentModel.DataAnnotations;

namespace MovieApp.Application.Feature.Movie.Dtos;

public class MovieCreateShowtimeRequest
{
    [Required] public string? Id { get; set; } // ID của phim

    [Required] public int Priority { get; set; } // Mức độ ưu tiên của phim

    public int Duration { get; set; } // Thời lượng của phim

    public DateOnly? ReleaseDate { get; set; } // Ngày phát hành của phim

    [Required] public long FormatId { get; set; }

    public int OriginalPriority { get; set; }

    public void resetPriority()
    {
        Priority = OriginalPriority;
    }

    public void decreasePriority()
    {
        Priority--;
    }
}
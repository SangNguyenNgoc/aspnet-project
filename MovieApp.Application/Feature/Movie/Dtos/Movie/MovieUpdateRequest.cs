using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MovieApp.Application.Feature.Movie.Dtos;

public class MovieUpdateRequest
{
    public string Name { get; set; }
    
    public string SubName { get; set; }
    
    public string Description { get; set; }
    
    public int AgeRestriction { get; set; }
    
    public string Director { get; set; }
    
    public DateOnly ReleaseDate { get; set; }
    
    public DateOnly EndDate { get; set; }
    
    public int RunningTime { get; set; }
    
    public IFormFile? Poster { get; set; }
    
    public string Producer { get; set; }
    
    public string Trailer { get; set; }
    
    public IFormFile? HorizontalPoster { get; set; }
    
    public string Language { get; set; }
    
    public string Performers { get; set; }
    
    public long Status { get; set; }
    
    public List<long> Genres { get; set; }

    public List<long> Formats { get; set; }
}
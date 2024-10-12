﻿using System.ComponentModel.DataAnnotations;
using aspdotnet_project.App.Movie.Dtos;

namespace aspdotnet_project.App.Show.Dtos;

public class CreateShowtimeRequest
{
    [Required]
    public string? CinemaId { get; set; }  // ID của rạp chiếu phim
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public List<MovieCreateShowtimeRequest> Movies { get; set; } 
}
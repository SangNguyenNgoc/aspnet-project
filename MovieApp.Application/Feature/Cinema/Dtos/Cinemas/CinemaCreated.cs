using System.ComponentModel.DataAnnotations;

namespace MovieApp.Application.Feature.Cinema.Dtos;

public class CinemaCreated
{
    [Required(ErrorMessage = "Address is required.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Hotline is required.")]
    [Phone(ErrorMessage = "Hotline must be a valid phone number.")]
    public string Hotline { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    public long Status { get; set; }

    [Required(ErrorMessage = "Location is required.")]
    public long Location { get; set; }
}
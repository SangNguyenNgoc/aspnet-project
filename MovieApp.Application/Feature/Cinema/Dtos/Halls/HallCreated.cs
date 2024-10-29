using System.ComponentModel.DataAnnotations;

namespace MovieApp.Application.Feature.Cinema.Dtos;

public class HallCreated
{
    [Required(ErrorMessage = "Number of rows is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Number of rows must be greater than 0.")]
    public int NumberOfRows { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Seats per row is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Seats per row must be greater than 0.")]
    public int SeatsPerRow { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    public long Status { get; set; }
    
    public List<SeatCreated> Seats { get; set; }
    
    public class SeatCreated
    {
        public int RowIndex { get; set; }
        public string RowName { get; set; }
        public bool Status { get; set; }
        public long Type { get; set; }
    }
}
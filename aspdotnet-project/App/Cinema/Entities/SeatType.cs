using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspdotnet_project.App.Cinema.Entities;

[Table(name:"seat_type")]
public class SeatType
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name:"name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    public double Price { get; set; }

    [InverseProperty("Type")]
    public virtual List<Seat> Seats { get; set; } = new List<Seat>();
}
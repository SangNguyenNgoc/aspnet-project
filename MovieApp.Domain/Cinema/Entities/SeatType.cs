using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Domain.Cinema.Entities;

[Table("seat_type")]
public class SeatType
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("name")] [StringLength(100)] public string Name { get; set; } = null!;

    [Column("price")] public long Price { get; set; }

    [InverseProperty("Type")] public virtual List<Seat> Seats { get; set; } = new();
}
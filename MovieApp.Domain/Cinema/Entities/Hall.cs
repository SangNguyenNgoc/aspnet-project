using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Domain.Cinema.Entities;

[Table("halls")]
public class Hall
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("number_of_rows", TypeName = "int(11)")]
    public int NumberOfRows { get; set; }

    [Column("name")] [StringLength(100)] public string Name { get; set; } = null!;

    [Column("seats_per_row", TypeName = "int(11)")]
    public int SeatsPerRow { get; set; }
    
    [Column("total_seats", TypeName = "int(11)")]
    public int TotalSeats { get; set; }

    [InverseProperty("Halls")] public required HallStatus Status { get; set; }

    [InverseProperty("Halls")] public required Cinema Cinema { get; set; }

    [InverseProperty("Hall")] public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    [InverseProperty("Hall")]
    public virtual ICollection<Show.Entities.Show> Shows { get; set; } = new List<Show.Entities.Show>();
}
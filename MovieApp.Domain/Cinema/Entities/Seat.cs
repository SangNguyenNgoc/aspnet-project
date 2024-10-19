using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieApp.Domain.Show.Entities;

namespace MovieApp.Domain.Cinema.Entities;

[Table("seats")]
public class Seat
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("row_index", TypeName = "int(11)")]
    public int RowIndex { get; set; }

    [Column("row_name")] [StringLength(1)] public string RowName { get; set; } = null!;

    [Column("order", TypeName = "int(11)")]
    public int Order { get; set; }

    [Column("status", TypeName = "bit(1)")]
    public bool Status { get; set; }

    [InverseProperty("Seats")] public required Hall Hall { get; set; }

    [InverseProperty("Seat")] public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    [InverseProperty("Seats")] public required SeatType Type { get; set; }
}
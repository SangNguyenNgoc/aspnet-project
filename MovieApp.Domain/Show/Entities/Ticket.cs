using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieApp.Domain.Cinema.Entities;

namespace MovieApp.Domain.Show.Entities;

[Table("tickets")]
public class Ticket
{
    [Key]
    [Column("id")]
    [StringLength(50)]
    public string Id { get; set; } = DateTime.Now.Ticks.ToString();

    [InverseProperty("Tickets")] public required Bill.Entities.Bill Bill { get; set; }

    [InverseProperty("Tickets")] public required Show Show { get; set; }

    [InverseProperty("Tickets")] public required Seat Seat { get; set; }
}
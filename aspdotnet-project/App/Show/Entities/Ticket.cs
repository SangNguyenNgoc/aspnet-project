using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using aspdotnet_project.App.Cinema.Entities;

namespace aspdotnet_project.App.Show.Entities;

[Table(name:"tickets")]
public class Ticket
{
    [Key]
    [Column(name: "id")]
    [StringLength(50)]
    public string Id { get; set; } = DateTime.Now.Ticks.ToString();

    [InverseProperty("Tickets")]
    public required Bill.entities.Bill Bill { get; set; }

    [InverseProperty("Tickets")]
    public required Show Show { get; set; }

    [InverseProperty("Tickets")]
    public required Seat Seat { get; set; }
    
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using aspdotnet_project.App.Show.Entities;

namespace aspdotnet_project.App.Cinema.Entities;

[Table(name:"seats")]
public class Seat
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Column(name:"row_index", TypeName = "int(11)")]
    public int RowIndex { get; set; }

    [Column(name:"row_name")]
    [StringLength(1)]
    public string RowName { get; set; } = null!;
    
    [Column(name:"order", TypeName = "int(11)")]
    public int Order { get; set; }

    [Column(name:"status", TypeName = "bit(1)")]
    public bool Status { get; set; }

    [InverseProperty("Seats")]
    public required Hall Hall { get; set; }

    [InverseProperty("Seat")] 
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
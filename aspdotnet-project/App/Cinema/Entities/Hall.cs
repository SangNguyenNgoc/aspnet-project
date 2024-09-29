using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspdotnet_project.App.Cinema.Entities;

[Table(name:"halls")]
public class Hall
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Column(name:"number_of_rows", TypeName = "int(11)")]
    public int NumberOfRows { get; set; }

    [Column(name:"name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;
    
    [Column(name:"seats_per_row", TypeName = "int(11)")]
    public int SeatsPerRow { get; set; }

    [InverseProperty("Halls")]
    public required HallStatus Status { get; set; }

    [InverseProperty("Halls")]
    public required Cinema Cinema { get; set; }

    [InverseProperty("Hall")] 
    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    [InverseProperty("Hall")]
    public virtual ICollection<Show.Entities.Show> Shows { get; set; } = new List<Show.Entities.Show>();

}
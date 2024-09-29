using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspdotnet_project.App.Cinema.Entities;

[Table(name:"hall_status")]
public class HallStatus
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name:"name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("Status")]
    public virtual ICollection<Hall> Halls { get; set; } = new List<Hall>();
}
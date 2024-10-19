using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Domain.Cinema.Entities;

[Table("hall_status")]
public class HallStatus
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("name")] [StringLength(100)] public string Name { get; set; } = null!;

    [InverseProperty("Status")] public virtual ICollection<Hall> Halls { get; set; } = new List<Hall>();
}
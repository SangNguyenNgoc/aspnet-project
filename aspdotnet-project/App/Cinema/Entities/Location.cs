using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspdotnet_project.App.Cinema.Entities;

[Table(name:"locations")]
public class Location
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Column(name:"name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;
    
    [Column("slug")]
    [StringLength(100)]
    public string Slug { get; set; } = null!;
    
    [InverseProperty("Location")]
    public virtual ICollection<Cinema> Cinemas { get; set; } = new List<Cinema>();
}
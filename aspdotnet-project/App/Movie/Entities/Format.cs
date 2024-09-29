using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspdotnet_project.App.Movie.Entities;

[Table(name:"formats")]
public class Format
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name:"caption")]
    [StringLength(20)]
    public string Caption { get; set; } = null!;

    [Column(name:"version")]
    [StringLength(20)]
    public string Version { get; set; } = null!;

    [Column(name:"slug")]
    [StringLength(20)]
    public string Slug { get; set; } = null!;

    [InverseProperty("Formats")]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    [InverseProperty("Format")]
    public virtual ICollection<Show.Entities.Show> Shows { get; set; } = new List<Show.Entities.Show>();

}
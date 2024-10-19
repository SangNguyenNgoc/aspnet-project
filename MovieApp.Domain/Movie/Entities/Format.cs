using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Domain.Movie.Entities;

[Table("formats")]
public class Format
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("caption")] [StringLength(20)] public string Caption { get; set; } = null!;

    [Column("version")] [StringLength(20)] public string Version { get; set; } = null!;

    [Column("slug")] [StringLength(20)] public string Slug { get; set; } = null!;

    [InverseProperty("Formats")] public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    [InverseProperty("Format")]
    public virtual ICollection<Show.Entities.Show> Shows { get; set; } = new List<Show.Entities.Show>();
}
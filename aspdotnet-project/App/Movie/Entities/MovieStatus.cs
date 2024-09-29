using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspdotnet_project.App.Movie.Entities;

[Table("movie_status")]
public class MovieStatus
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("description")]
    [StringLength(50)]
    public string Description { get; set; } = null!;

    [Column("slug")]
    [StringLength(20)]
    public string Slug { get; set; } = null!;

    [InverseProperty("Status")]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
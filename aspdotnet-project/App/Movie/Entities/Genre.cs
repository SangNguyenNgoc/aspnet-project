using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspdotnet_project.App.Movie.Entities;

[Table(name:"genres")]
public class Genre
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name:"name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("Genres")]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
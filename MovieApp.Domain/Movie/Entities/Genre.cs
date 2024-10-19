using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Domain.Movie.Entities;

[Table("genres")]
public class Genre
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("name")] [StringLength(50)] public string Name { get; set; } = null!;

    [InverseProperty("Genres")] public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspdotnet_project.App.Movie.Entities;

[Table("images")]
public class Image
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name: "url")] 
    public string Url { get; set; } = null!;

    [InverseProperty("Images")]
    public required Movie Movie { get; set; }
}
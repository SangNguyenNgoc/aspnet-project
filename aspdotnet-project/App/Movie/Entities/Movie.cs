using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspdotnet_project.App.Movie.Entities;

[Table("movies")]
public class Movie
{
    [Key]
    [Column(name: "id")]
    [StringLength(50)]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;
    
    [Column("sub_name")]
    [StringLength(100)]
    public string SubName { get; set; } = null!;
    
    [Column("slug")]
    [StringLength(100)]
    public string Slug { get; set; } = null!;
    
    [Column(name:"description", TypeName = "text")]
    [StringLength(2000)]
    public string Description { get; set; } = null!;
    
    [Column(name:"age_restriction", TypeName = "int(11)")]
    public int AgeRestriction { get; set; }

    [Column("director")]
    [StringLength(255)]
    public string Director { get; set; } = null!;
    
    [Column("release_date")]
    public DateOnly ReleaseDate { get; set; }

    [Column("end_date")]
    public DateOnly EndDate { get; set; }

    [Column(name:"running_time", TypeName = "int(11)")]
    public int RunningTime { get; set; }
    
    [Column("horizontal_poster")]
    [StringLength(500)]
    public string HorizontalPoster { get; set; } = null!;

    [Column("language")]
    [StringLength(50)]
    public string Language { get; set; } = null!;

    [Column(name:"sum_of_ratings", TypeName = "int(11)")]
    public int SumOfRatings { get; set; }

    [Column(name:"number_of_ratings", TypeName = "int(11)")]
    public int NumberOfRatings { get; set; }

    [Column("performers")]
    [StringLength(200)]
    public string Performers { get; set; } = null!;

    [Column("poster")]
    [StringLength(200)]
    public string Poster { get; set; } = null!;

    [Column("producer")]
    [StringLength(100)]
    public string Producer { get; set; } = null!;

    [Column("trailer")]
    [StringLength(100)]
    public string Trailer { get; set; } = null!;

    [InverseProperty("Movies")]
    public required MovieStatus Status { get; set; }

    [InverseProperty("Movie")]
    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    [InverseProperty("Movies")] 
    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

    [InverseProperty("Movies")] 
    public virtual ICollection<Format> Formats { get; set; } = new List<Format>();

    [InverseProperty("Movie")]
    public virtual ICollection<Show.Entities.Show> Shows { get; set; } = new List<Show.Entities.Show>();
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieApp.Domain.Cinema.Entities;
using MovieApp.Domain.Movie.Entities;
using MovieApp.Domain.Show.Entities;

// ReSharper disable All

namespace MovieApp.Domain.Show.Entities;

[Table(name: "shows")]
public class Show
{
    [Key]
    [Column(name: "id")]
    [StringLength(50)]
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column(name: "running_time", TypeName = "int(11)")]
    public int RunningTime { get; set; }

    [Column("start_date")] public DateOnly StartDate { get; set; }

    [Column("start_time")] public TimeOnly StartTime { get; set; }

    [Column(name: "status", TypeName = "bit(1)")]
    public ulong Status { get; set; }

    [InverseProperty("Shows")] public required Movie.Entities.Movie Movie { get; set; }

    [InverseProperty("Shows")] public required Format Format { get; set; }

    [InverseProperty("Shows")] public required Hall Hall { get; set; }

    [InverseProperty("Show")] public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
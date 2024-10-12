﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspdotnet_project.App.Cinema.Entities;

[Table(name:"cinemas")]
public class Cinema
{
    [Key]
    [Column(name:"id")]
    [StringLength(50)]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Column(name:"address")]
    [StringLength(200)]
    public string Address { get; set; } = null!;

    [Column(name: "description", TypeName = "text")]
    [StringLength(2000)]
    public string Description { get; set; } = null!;

    [Column(name:"name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column(name:"hotline")]
    [StringLength(20)]
    public string? Hotline { get; set; }

    [StringLength(255)]
    public string Slug { get; set; } = null!;

    [InverseProperty("Cinemas")]
    public required CinemaStatus Status { get; set; }

    [InverseProperty("Cinema")]
    public virtual ICollection<Hall> Halls { get; set; } = new List<Hall>();
    
    [InverseProperty("Cinemas")]
    public Location Location { get; set; }
}
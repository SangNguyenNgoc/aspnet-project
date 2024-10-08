﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using aspdotnet_project.App.Show.Entities;

namespace aspdotnet_project.App.Bill.entities;

[Table(name: "bills")]
// ReSharper disable once ClassNeverInstantiated.Global
public class Bill
{
    [Key]
    [Column(name:"id")]
    [StringLength(50)]
    public string Id { get; set; } = DateTime.Now.Ticks.ToString();

    [Column(name:"expire_at")]
    public DateTime ExpireAt { get; set; }

    [Column(name:"failure_reason")]
    [StringLength(50)]
    public string FailureReason { get; set; } = string.Empty;

    [Column(name:"payment_at")]
    public DateTime PaymentAt { get; set; }

    [Column(name:"payment_url")]
    [StringLength(500)]
    public string PaymentUrl { get; set; } = string.Empty;

    [Column(name:"total")]
    public long Total { get; set; }

    [InverseProperty("Bills")]
    public required BillStatus Status { get; set; }
    
    [InverseProperty("Bills")]
    public required User.Entities.User User { get; set; }

    [InverseProperty("Bill")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

}
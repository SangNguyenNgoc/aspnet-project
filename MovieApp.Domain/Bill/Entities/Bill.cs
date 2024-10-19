using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieApp.Domain.Show.Entities;

namespace MovieApp.Domain.Bill.Entities;

[Table("bills")]
// ReSharper disable once ClassNeverInstantiated.Global
public class Bill
{
    [Key]
    [Column("id")]
    [StringLength(50)]
    public string Id { get; set; } = DateTime.Now.Ticks.ToString();

    [Column("expire_at")] public DateTime ExpireAt { get; set; }

    [Column("failure_reason")]
    [StringLength(50)]
    public string FailureReason { get; set; } = string.Empty;

    [Column("payment_at")] public DateTime PaymentAt { get; set; }

    [Column("payment_url")]
    [StringLength(500)]
    public string PaymentUrl { get; set; } = string.Empty;

    [Column("total")] public long Total { get; set; }

    [InverseProperty("Bills")] public required BillStatus Status { get; set; }

    [InverseProperty("Bills")] public required User.Entities.User User { get; set; }

    [InverseProperty("Bill")] public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
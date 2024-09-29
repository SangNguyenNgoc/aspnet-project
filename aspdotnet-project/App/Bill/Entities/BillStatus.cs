using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspdotnet_project.App.Bill.entities;

[Table("bill_status")]
// ReSharper disable once ClassNeverInstantiated.Global
public class BillStatus
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name:"name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;
    
    [InverseProperty("Status")]
    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
    
}
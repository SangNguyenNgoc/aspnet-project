using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace aspdotnet_project.App.User.Entities;

[Table("users")]
public class User : IdentityUser
{
    [MaxLength(6)]
    [Column(name:"create_date")]
    public DateTime CreateDate { get; set; }

    [StringLength(255)]
    [Column(name:"avatar")]
    public string Avatar { get; set; } = null!;

    [Column(name:"date_of_birth")]
    public DateOnly? DateOfBirth { get; set; }

    [Column(name:"fullname")]
    [StringLength(100)]
    public string FullName { get; set; } = null!;

    [Column(name:"gender", TypeName = "enum('Female','Male','Unknown')")]
    public Gender Gender { get; set; }
    
    [InverseProperty("User")]
    public virtual ICollection<Bill.entities.Bill> Bills { get; set; } = new List<Bill.entities.Bill>();
    
}
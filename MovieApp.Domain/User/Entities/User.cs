using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MovieApp.Domain.User.Entities;

[Table("users")]
public class User : IdentityUser
{
    [MaxLength(6)] [Column("create_date")] public DateTime CreateDate { get; set; }

    [StringLength(255)] [Column("avatar")] public string Avatar { get; set; } = null!;

    [Column("date_of_birth")] public DateOnly? DateOfBirth { get; set; }

    [Column("fullname")]
    [StringLength(100)]
    public string? FullName { get; set; } = null!;

    [Column("gender", TypeName = "enum('Female','Male','Unknown')")]
    public Gender Gender { get; set; }

    [Column("email_change_token")] public string? ChangeToken { get; set; }

    [Column("new_email")] public string? NewEmail { get; set; }

    // 0: inactive, 1: active, 2: locked
    [Column("status")] public int Status { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Bill.Entities.Bill> Bills { get; set; } = new List<Bill.Entities.Bill>();
}
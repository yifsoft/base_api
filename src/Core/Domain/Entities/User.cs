using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User : BaseEntity
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string EmailHash { get; set; }

    [Required]
    public string PhoneHash { get; set; }

    public bool EmailVerified { get; set; }
    public bool PhoneVerified { get; set; }
    public bool IsActive { get; set; }

    public string RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [Required]
    public byte[] PasswordHash { get; set; }

    [Required]
    public byte[] PasswordSalt { get; set; }


    public User()
    {
        EmailVerified = false;
        PhoneVerified = false;
        IsActive = false;
    }
}


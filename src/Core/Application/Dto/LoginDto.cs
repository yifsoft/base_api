using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dto;

public class LoginDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [JsonIgnore]
    public bool RememberMe { get; set; }
}
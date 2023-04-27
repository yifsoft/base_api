using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class OperationClaim : BaseEntity
{
    [Required]
    public string Name { get; set; }
}

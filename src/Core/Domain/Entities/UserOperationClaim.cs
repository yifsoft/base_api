using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class UserOperationClaim : BaseEntity
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int OperationClaimId { get; set; }

    public virtual ICollection<OperationClaim> OperationClaims { get; set; }
    public virtual User User { get; set; }
}

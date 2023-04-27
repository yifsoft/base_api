using Domain.Common;
using Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class Category : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Slug { get; set; }

    [Required]
    public string MetaTags { get; set; }

    [Required]
    public Language Language { get; set; }
    public virtual ICollection<Post> Posts { get; set; }
}

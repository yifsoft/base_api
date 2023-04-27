using Domain.Common;
using Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Post : BaseEntity
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Body { get; set; }

    [Required]
    public string Slug { get; set; }

    [Required]
    public string MetaTags { get; set; }

    [Required]
    public Language Language { get; set; }

    public DateTime? PublishedAt { get; set; }


    public virtual User User { get; set; }
    public virtual Category Category { get; set; }

}

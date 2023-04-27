using Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto;
public class PostDto
{

    public int? Id { get; set; }

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
}

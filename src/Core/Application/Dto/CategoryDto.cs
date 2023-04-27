using Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto;
public class CategoryDto
{
    public int? Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Slug { get; set; }

    [Required]
    public string MetaTags { get; set; }

    [Required]
    public Language Language { get; set; }
}


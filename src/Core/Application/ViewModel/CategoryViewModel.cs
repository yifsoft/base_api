using Domain.Common.Enums;

namespace Application.ViewModel;

public class CategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Slug { get; set; }

    public string MetaTags { get; set; }

    public Language Language { get; set; }

    public virtual IEnumerable<PostViewModel> Posts { get; set; }
}

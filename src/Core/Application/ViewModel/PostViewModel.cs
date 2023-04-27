using Domain.Common.Enums;

namespace Application.ViewModel
{
    public class PostViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CategoryId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Slug { get; set; }

        public string Meta { get; set; }

        public Language Language { get; set; }

        public DateTime CreatedAt { get; set; }


        public virtual UserViewModel User { get; set; }
    }
}

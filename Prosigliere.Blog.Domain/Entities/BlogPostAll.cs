namespace Prosigliere.Blog.Domain.Entities
{
    public class BlogPostAll
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CommentsCount { get; set; }
    }
}

namespace Prosigliere.Blog.Domain.Entities
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CommentsCount { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}

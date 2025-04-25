using Prosigliere.Blog.Domain.Entities;

namespace Prosigliere.Blog.Domain.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<List<BlogPostAll>> GetAllPosts();
        Task<BlogPost> GetPostById(int id);
        Task AddPost(BlogPost post);
        Task AddCommentToPost(int postId, Comment comment);
    }
}

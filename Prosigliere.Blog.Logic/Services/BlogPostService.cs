using Prosigliere.Blog.Domain.Entities;
using Prosigliere.Blog.Domain.Interfaces;

namespace Prosigliere.Blog.Logic.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public async Task<List<BlogPostAll>> GetAllPosts() => await  _blogPostRepository.GetAllPosts();

        public async Task<BlogPost> GetPostById(int id) => await _blogPostRepository.GetPostById(id);

        public async Task AddPost(BlogPost post) => await _blogPostRepository.AddPost(post);

        public async Task AddCommentToPost(int postId, Comment comment) => await _blogPostRepository.AddCommentToPost(postId, comment);
    }
}

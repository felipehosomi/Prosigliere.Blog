using Newtonsoft.Json;
using Prosigliere.Blog.Domain.Entities;
using Prosigliere.Blog.Domain.Interfaces;

namespace Prosigliere.Blog.Infrastructure.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly string _filePath = "blog.json"; // JSON file path

        public BlogPostRepository()
        {
            // Ensure the file exists at startup
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]"); // Create an empty JSON file
            }
        }

        private async Task<List<BlogPost>> LoadPosts()
        {
            string jsonData = await File.ReadAllTextAsync(_filePath);
            return JsonConvert.DeserializeObject<List<BlogPost>>(jsonData) ?? new List<BlogPost>();
        }

        private async Task<List<BlogPostAll>> LoadAllPosts()
        {
            string jsonData = await File.ReadAllTextAsync(_filePath);
            return JsonConvert.DeserializeObject<List<BlogPostAll>>(jsonData) ?? new List<BlogPostAll>();
        }

        private async Task SavePosts(List<BlogPost> posts)
        {
            string jsonData = JsonConvert.SerializeObject(posts, Formatting.Indented);
            await File.WriteAllTextAsync(_filePath, jsonData);
        }

        public async Task<List<BlogPostAll>> GetAllPosts()
        {
            return await LoadAllPosts();
        }

        public async Task<BlogPost> GetPostById(int id)
        {
            List<BlogPost> postList = await LoadPosts();
            return postList.FirstOrDefault(p => p.Id == id);
        }

        public async Task AddPost(BlogPost post)
        {
            List<BlogPost> posts = await LoadPosts();
            post.Id = posts.Count > 0 ? posts.Max(p => p.Id) + 1 : 1;
            if (post.Comments != null)
            {
                post.CommentsCount = post.Comments.Count;
                int i = 0;
                foreach (Comment comment in post.Comments)
                {
                    comment.Id = ++i;
                }
            }

            posts.Add(post);
            await SavePosts(posts);
        }

        public async Task AddCommentToPost(int postId, Comment comment)
        {
            List<BlogPost> posts = await LoadPosts();
            BlogPost post = posts.FirstOrDefault(p => p.Id == postId);
            if (post != null)
            {
                comment.Id = post.Comments.Count > 0 ? post.Comments.Max(c => c.Id) + 1 : 1; // Assign new comment ID
                post.Comments.Add(comment);
                await SavePosts(posts);
            }
        }
    }
}

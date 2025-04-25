using Prosigliere.Blog.Domain.Entities;
using Prosigliere.Blog.Infrastructure.Repository;

namespace Prosigliere.Blog.Tests.Infrastructure
{
    public class BlogPostRepositoryTests
    {
        private const string TestFilePath = "test_blogposts.json";

        public BlogPostRepositoryTests()
        {
            // Ensure that the test file is fresh for each test
            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }
        }

        [Fact]
        public async Task AddPost_ShouldAddPostSuccessfully()
        {
            BlogPostRepository repository = new BlogPostRepository();

            BlogPost newPost = new BlogPost { Title = "Test Title", Content = "Test Content" };
            await repository.AddPost(newPost);

            BlogPost fetchedPost = await repository.GetPostById(newPost.Id);
            Assert.NotNull(fetchedPost);
            Assert.Equal("Test Title", fetchedPost.Title);
        }

        [Fact]
        public async Task GetAllPosts_ShouldReturnAllPosts()
        {
            BlogPostRepository repository = new BlogPostRepository();

            await repository.AddPost(new BlogPost { Title = "Test Post 1", Content = "Content 1" });
            await repository.AddPost(new BlogPost { Title = "Test Post 2", Content = "Content 2" });

            List<BlogPostAll> posts = await repository.GetAllPosts();
            Assert.Equal(2, posts.Count);
        }

        [Fact]
        public async Task GetPostById_ShouldReturnCorrectPost()
        {
            BlogPostRepository repository = new BlogPostRepository();
            BlogPost post = new BlogPost { Title = "Test Post", Content = "Some Content" };
            await repository.AddPost(post);

            BlogPost fetchedPost = await repository.GetPostById(post.Id);
            Assert.NotNull(fetchedPost);
            Assert.Equal("Test Post", fetchedPost.Title);
        }

        [Fact]
        public async Task AddCommentToPost_ShouldAddCommentSuccessfully()
        {
            BlogPostRepository repository = new BlogPostRepository();
            BlogPost post = new BlogPost { Title = "Test Post", Content = "Some Content" };
            await repository.AddPost(post);

            Comment comment = new Comment { Author = "Author", Content = "Test Comment" };
            await repository.AddCommentToPost(post.Id, comment);

            BlogPost fetchedPost = await repository.GetPostById(post.Id);
            Assert.Single(fetchedPost.Comments);
            Assert.Equal("Test Comment", fetchedPost.Comments[0].Content);
        }
    }
}

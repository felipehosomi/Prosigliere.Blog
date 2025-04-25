using Moq;
using Prosigliere.Blog.Domain.Entities;
using Prosigliere.Blog.Domain.Interfaces;
using Prosigliere.Blog.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prosigliere.Blog.Tests.Logic
{
    public class BlogPostServiceTests
    {
        private readonly Mock<IBlogPostRepository> _mockRepository;
        private readonly IBlogPostService _service;

        public BlogPostServiceTests()
        {
            _mockRepository = new Mock<IBlogPostRepository>();
            _service = new BlogPostService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllPostsAsync_ShouldReturnAllPosts()
        {
            List<BlogPostAll> posts = new List<BlogPostAll>
            {
                new BlogPostAll { Id = 1, Title = "Post 1", Content = "Content 1" },
                new BlogPostAll { Id = 2, Title = "Post 2", Content = "Content 2" }
            };

            _mockRepository.Setup(repo => repo.GetAllPosts()).ReturnsAsync(posts);

            List<BlogPostAll> result = await _service.GetAllPosts();
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task AddPostAsync_ShouldAddPost()
        {
            BlogPost newPost = new BlogPost { Title = "New Post", Content = "New Content" };

            await _service.AddPost(newPost);
            _mockRepository.Verify(repo => repo.AddPost(newPost), Times.Once);
        }
    }
}

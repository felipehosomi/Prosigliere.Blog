using Microsoft.AspNetCore.Mvc;
using Prosigliere.Blog.Domain.Entities;
using Prosigliere.Blog.Domain.Interfaces;

namespace Prosigliere.Blog.WebApi.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BlogPostAll>>> GetAllPosts()
        {
            return Ok(await _blogPostService.GetAllPosts());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] BlogPost post)
        {
            await _blogPostService.AddPost(post);
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetPostById(int id)
        {
            BlogPost post = await _blogPostService.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> AddComment(int id, [FromBody] Comment comment)
        {
            await _blogPostService.AddCommentToPost(id, comment);
            return NoContent();
        }
    }
}

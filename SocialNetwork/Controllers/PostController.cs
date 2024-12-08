using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dtos.Post;
using SocialNetwork.Interfaces;

namespace SocialNetwork.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // var userId = UserHelper.GetUserId(User);
            var userId = 1;

            var result = await _postService.CreatePostAsync(createPostDto, userId);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _postService.UpdatePostAsync(id, updateDto);

            if (!result) return BadRequest();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _postService.DeletePostAsync(id);

            if (!result) return BadRequest();

            return Ok(result);
        }
    }
}
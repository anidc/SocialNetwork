using System.Security.Claims;
using FirstCast.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dtos.Post;
using SocialNetwork.Helpers;
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var posts = await _postService.GetAllPostsAsync(userId);
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var post = await _postService.GetPostByIdAsync(id);

            return Ok(post);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = ClaimsHelper.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) throw ExceptionManager.NotAuthorized();

            var result = await _postService.CreatePostAsync(createPostDto, userId);

            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = ClaimsHelper.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) throw ExceptionManager.NotAuthorized();

            var result = await _postService.UpdatePostAsync(id, updateDto, userId);

            if (!result) return BadRequest();

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = ClaimsHelper.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) throw ExceptionManager.NotAuthorized();

            var result = await _postService.DeletePostAsync(id, userId);

            if (!result) return BadRequest();

            return Ok(result);
        }

        [Authorize]
        [HttpPost("{postId}/like")]
        public async Task<IActionResult> TogglePost(int postId)
        {
            var userId = ClaimsHelper.GetUserId(User);

            var result = await _postService.ToggleLike(postId, userId);

            return Ok(result);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dtos.Comment;
using SocialNetwork.Interfaces;

namespace SocialNetwork.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var comments = await _commentService.GetAllCommentsAsync();
            
            return Ok(comments);
        }

        [HttpPost("{postId}")]
        public async Task<IActionResult> CreateComment([FromRoute] int postId, [FromBody] CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            // var userId = UserHelper.GetUserId(User);
            var userId = 1;

            var result = await _commentService.CreateCommentAsync(createCommentDto, userId, postId);

            return Ok(result);
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int commentId, [FromBody] UpdateCommentDto updateCommentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // var userId = UserHelper.GetUserId(User);
            var userId = 1;

            var comment = await _commentService.UpdateCommentAsync(commentId, updateCommentDto, userId);

            return Ok(comment);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var comment = await _commentService.DeleteCommentAsync(commentId);

            return Ok(comment);
        }
    }
}
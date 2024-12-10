using FirstCast.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dtos.Comment;
using SocialNetwork.Helpers;
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
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var comments = await _commentService.GetAllCommentsAsync();

            return Ok(comments);
        }

        [HttpPost("{postId}")]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromRoute] int postId,
            [FromBody] CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = ClaimsHelper.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) throw ExceptionManager.NotAuthorized();

            var result = await _commentService.CreateCommentAsync(createCommentDto, userId, postId);

            return Ok(result);
        }

        [HttpPut("{commentId}")]
        [Authorize]
        public async Task<IActionResult> UpdateComment([FromRoute] int commentId,
            [FromBody] UpdateCommentDto updateCommentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = ClaimsHelper.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) throw ExceptionManager.NotAuthorized();

            var comment = await _commentService.UpdateCommentAsync(commentId, updateCommentDto, userId);

            return Ok(comment);
        }

        [HttpDelete("{commentId}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = ClaimsHelper.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) throw ExceptionManager.NotAuthorized();

            var comment = await _commentService.DeleteCommentAsync(commentId, userId);

            return Ok(comment);
        }
    }
}
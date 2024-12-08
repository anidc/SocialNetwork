using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dtos.Comment;
using SocialNetwork.Interfaces;
using SocialNetwork.Mappers;
using SocialNetwork.Models;
using SocialNetwork.Repository;

namespace SocialNetwork.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;
        public CommentController(ICommentService commentService, IPostService postService)
        {
            _commentService = commentService;
            _postService = postService;
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

            var comment = await _commentService.UpdateCommentAsync(commentId, updateCommentDto);

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
using Microsoft.AspNetCore.Http.HttpResults;
using SocialNetwork.Dtos.Comment;
using SocialNetwork.Interfaces;
using SocialNetwork.Mappers;

namespace SocialNetwork.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostService _postService;

        public CommentService(ICommentRepository commentRepository, IPostService postService)
        {
            _commentRepository = commentRepository;
            _postService = postService;
        }


        public async Task<List<CommentDto>> GetAllCommentsAsync()
        {
            var comment = await _commentRepository.GetAllCommentsAsync();

            var commentDto = comment.Select(c => c.ToCommentDto()).ToList();

            return commentDto;
        }

        public async Task<bool> CreateCommentAsync(CreateCommentDto createCommentDto, int userId, int postId)
        {

            if (!await _postService.PostExists(postId))
            {
                throw new Exception("Stock doesn't exist");
            }

            var comment = CommentMapper.ToCommentFromCreate(createCommentDto, postId);

            //comment.UserId = userId;

            return await _commentRepository.CreateCommentAsync(comment);
        }

        public async Task<bool> UpdateCommentAsync(int id, UpdateCommentDto updateDto)
        {
            var update = CommentMapper.ToCommentFromUpdate(updateDto);

            return await _commentRepository.UpdateCommentAsync(id, update); 
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            return await _commentRepository.DeleteCommentAsync(id);
        }
    }
}

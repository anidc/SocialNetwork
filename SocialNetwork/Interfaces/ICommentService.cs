using SocialNetwork.Dtos;
using SocialNetwork.Dtos.Comment;

namespace SocialNetwork.Interfaces
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAllCommentsAsync();
        //Task<CommentDto?> GetCommentByIdAsync(int id);
        Task<bool> CreateCommentAsync(CreateCommentDto createCommentDto, int userId, int postId);
        Task<bool> UpdateCommentAsync(int commentId, UpdateCommentDto updateDto, int userId);
        Task<bool> DeleteCommentAsync(int commentId);
    }
}

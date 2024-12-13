using SocialNetwork.Dtos.Comment;

namespace SocialNetwork.Interfaces
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAllCommentsAsync();
        Task<List<CommentDto>> GetAllCommentsByPostAsync(int postId);

        //Task<CommentDto?> GetCommentByIdAsync(int id);
        Task<bool> CreateCommentAsync(CreateCommentDto createCommentDto, string userId, int postId);
        Task<bool> UpdateCommentAsync(int commentId, UpdateCommentDto updateDto, string userId);
        Task<bool> DeleteCommentAsync(int commentId, string userId);
    }
}
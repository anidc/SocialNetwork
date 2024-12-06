using SocialNetwork.Dtos;
using SocialNetwork.Dtos.Comment;

namespace SocialNetwork.Interfaces
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAllCommentsAsync();
        //Task<CommentDto?> GetCommentByIdAsync(int id);
        Task<bool> CreateCommentAsync(CreateCommentDto createCommentDto, int userId, int postId);
        Task<bool> UpdateCommentAsync(int id, UpdateCommentDto updateDto);
        //Task<bool> DeleteCommentAsync(int id);
    }
}

using SocialNetwork.Dtos;
using SocialNetwork.Dtos.Post;

namespace SocialNetwork.Interfaces
{
    public interface IPostService
    {
        Task<bool> CreatePostAsync(CreatePostDto createPostDto, string userId);
        Task<List<PostDto>> GetAllPostsAsync();
        Task<PostDto?> GetPostByIdAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdatePostDto updateDto);
        Task<bool> DeletePostAsync(int id);
        Task<bool> PostExists(int id);
    }
}
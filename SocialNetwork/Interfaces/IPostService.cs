using SocialNetwork.Dtos;
using SocialNetwork.Dtos.Post;

namespace SocialNetwork.Interfaces
{
    public interface IPostService
    {
        Task<bool> CreatePostAsync(CreatePostDto createPostDto, string userId);
        Task<List<PostDto>> GetAllPostsAsync();
        Task<PostDto> GetPostByIdAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdatePostDto updateDto, string userId);
        Task<bool> DeletePostAsync(int id, string userId);
        Task<bool> PostExists(int id);
    }
}
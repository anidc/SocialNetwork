using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Dtos;
using SocialNetwork.Dtos.Post;

namespace SocialNetwork.Interfaces
{
    public interface IPostService
    {
        Task<bool> CreatePostAsync(CreatePostDto createPostDto, int userId);
        Task<List<PostDto>> GetAllPostsAsync();
        Task<PostDto?> GetPostByIdAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdatePostDto updateDto);
        Task<bool> DeletePostAsync(int id);
        Task<bool> PostExists(int id);

    }
}
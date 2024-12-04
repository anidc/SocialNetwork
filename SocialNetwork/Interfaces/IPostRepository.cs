using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Dtos.Post;
using SocialNetwork.Models;

namespace SocialNetwork.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllAsync();
        Task<Post> GetByIdAsync(int id);
        Task<Post> CreateAsync(Post post);
        Task<Post> UpdateAsync(int id, UpdatePostDto post);
        Task<Post> DeleteAsync(Post post);
    }
}
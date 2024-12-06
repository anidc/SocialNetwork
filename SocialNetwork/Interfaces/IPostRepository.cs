using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Models;

namespace SocialNetwork.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<Post?> GetByIdAsync(int id);
        Task<bool> CreateAsync(Post post);
        Task<bool> UpdateAsync(Post post);
        Task<bool> DeleteAsync(int id);
        Task<bool> PostExists(int id);
    }
}
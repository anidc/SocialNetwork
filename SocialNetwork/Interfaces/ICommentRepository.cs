using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Models;

namespace SocialNetwork.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync();
        Task<List<Comment>> GetAllCommentsByPostAsync(int postId);
        Task<bool> CreateCommentAsync(Comment comment);
        Task<bool> UpdateCommentAsync(int id, Comment comment);
        Task<bool> DeleteCommentAsync(int id);
        Task<Comment?> GetCommentByIdAsync(int commentId);
    }
}
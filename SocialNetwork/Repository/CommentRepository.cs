using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Interfaces;
using SocialNetwork.Models;

namespace SocialNetwork.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            var changes = await _context.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            var comments = await _context.Comments.Where(p => p.IsDeleted != true).ToListAsync();

            return comments;
        }

        public async Task<bool> UpdateCommentAsync(int id, Comment comment)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (existingComment == null) throw new Exception("Comment with this id doesnt exist");

            existingComment.Text = comment.Text;
            existingComment.UpdatedAt = DateTime.Now;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (existingComment == null) throw new Exception("Comment with this id doesnt exist");
            existingComment.IsDeleted = true;
            existingComment.DeletedAt = DateTime.Now;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Comment?> GetCommentByIdAsync(int commentId)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId && c.IsDeleted == false);
        }
    }
}
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

    }
}
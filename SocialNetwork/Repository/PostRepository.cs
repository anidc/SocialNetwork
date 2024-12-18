using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Dtos.Post;
using SocialNetwork.Interfaces;
using SocialNetwork.Models;

namespace SocialNetwork.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDBContext _context;

        public PostRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            var posts = await _context.Posts
                .Include(u => u.User)
                .Where(p=> !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
            
            return posts;
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            var post = await _context.Posts.Include(u => u.User).FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted != true);
    
            return post;
        }

        public async Task<bool> CreateAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            var changes = await _context.SaveChangesAsync();
            
            return changes > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var postModel = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if(postModel == null) return false;

            postModel.DeletedAt = DateTime.Now;
            postModel.IsDeleted = true;
            
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Post post)
        {
            var existingPostEntity = await _context.Posts.FirstOrDefaultAsync(p => p.Id == post.Id);

            if(existingPostEntity == null) throw new Exception("");

            existingPostEntity.Content = post.Content;
            existingPostEntity.UpdatedAt = DateTime.Now; 

            return (await _context.SaveChangesAsync()) > 0;
        }

        public Task<bool> PostExists(int id)
        {
            return _context.Posts.AnyAsync(p => p.Id == id);
        }
    }
}
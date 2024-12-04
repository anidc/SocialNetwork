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
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDBContext _context;
        public PostRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            var posts = await _context.Posts.ToListAsync();

            if(posts == null) return null;
            
            return posts;
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);

            if(post == null) return null;
            
            return post;
        }

        public async Task<Post> CreateAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public Task<Post> DeleteAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<Post> UpdateAsync(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
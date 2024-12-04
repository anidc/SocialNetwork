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

        public async Task<Post> DeleteAsync(int id)
        {
            var postModel = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if(postModel == null) return null;

            postModel.DeletedAt = DateTime.Now;
            postModel.IsDeleted = true;
            
            await _context.SaveChangesAsync();
            return postModel;
        }

        public async Task<Post> UpdateAsync(int id, UpdatePostDto post)
        {
            var existingPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);

            if(existingPost == null) return null;

            existingPost.Content = post.Content;
            existingPost.UpdatedAt = DateTime.Now; 

            await _context.SaveChangesAsync();

            return existingPost;
        }
    }
}
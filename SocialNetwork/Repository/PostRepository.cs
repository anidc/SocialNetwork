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
    }
}
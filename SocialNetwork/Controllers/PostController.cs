using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Interfaces;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IPostRepository _postRepo;
        public PostController(ApplicationDBContext context, IPostRepository postRepository)
        {
            _context = context;
            _postRepo = postRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var posts = await _postRepo.GetAllAsync();
            return Ok(posts);
        }
    }
}
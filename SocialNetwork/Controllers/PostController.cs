using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Dtos.Post;
using SocialNetwork.Interfaces;
using SocialNetwork.Mappers;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepo;
        public PostController(IPostRepository postRepository)
        {
            _postRepo = postRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var posts = await _postRepo.GetAllAsync();
            return Ok(posts);
        }

        [HttpGet("{id:int}")] 

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var post = await _postRepo.GetByIdAsync(id);
            if(post == null) return NotFound();
            return Ok(post);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreatePostDto post)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            
            var newPost = post.ToPostFromPostDto();

            await _postRepo.CreateAsync(newPost);

            return CreatedAtAction(nameof(GetById), new {id = newPost.Id}, newPost.ToPostDto());

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePostDto updateDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var post = await _postRepo.UpdateAsync(id, updateDto);

            if(post == null) return NotFound();

            return Ok(post.ToPostDto());
        }
        
    }
}
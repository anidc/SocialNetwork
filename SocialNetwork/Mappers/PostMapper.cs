using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Dtos;
using SocialNetwork.Dtos.Post;
using SocialNetwork.Models;

namespace SocialNetwork.Mappers
{
    public static class PostMapper
    {
        public static PostDto ToPostDto(this Post post)
        {
            return new PostDto 
            { 
                Id = post.Id,
                Content = post.Content,
                Likes = post.Likes.Count,
                User = post.User.ToUserDto(),
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt
            };
        }
        public static Post ToPostFromPostDto(this CreatePostDto postDto) 
        {
            return new Post 
            { 
                Content = postDto.Content
            };
        } 
    }
}
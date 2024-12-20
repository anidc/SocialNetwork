﻿using SocialNetwork.Dtos.Comment;
using SocialNetwork.Dtos.Post;
using SocialNetwork.Models;

namespace SocialNetwork.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment) 
        {
            return new CommentDto
            {
                Id = comment.Id,
                PostId = comment.PostId,
                Text = comment.Text,
                User = comment.User.ToUserDto(),
                UpdatedAt = comment.UpdatedAt,
                CreatedAt = comment.CreatedAt
            };
        }

        public static Comment ToCommentFromCommentDto(this CreateCommentDto commentDto)
        {
            return new Comment
            {
                
                Text = commentDto.Text
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int postId)
        {
            return new Comment
            {
                Text = commentDto.Text,
                PostId = postId
            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentDto commentDto)
        {
            return new Comment
            {
                Text = commentDto.Text
            };
        }
    }
}

using SocialNetwork.Dtos.Comment;
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
                PostId = comment.PostId,
                Text = comment.Text,
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
    }
}

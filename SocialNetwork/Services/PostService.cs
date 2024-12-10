using FirstCast.Application.Services;
using SocialNetwork.Dtos;
using SocialNetwork.Dtos.Post;
using SocialNetwork.Interfaces;
using SocialNetwork.Mappers;
using SocialNetwork.Models;

namespace SocialNetwork.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<bool> CreatePostAsync(CreatePostDto createPostDto, string userId)
        {
            if (createPostDto == null)
                throw new ArgumentNullException(nameof(createPostDto), "Post data cannot be null.");
            var post = createPostDto.ToPostFromPostDto();
            post.UserId = userId;

            return await _postRepository.CreateAsync(post);
        }

        public async Task<List<PostDto>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllPostsAsync();

            var postDtos = posts.Select(p => p.ToPostDto());

            return postDtos.ToList();
        }

        public async Task<PostDto?> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);

            if (post == null) throw ExceptionManager.NotFound(nameof(Post), id.ToString());

            return post.ToPostDto();
        }

        public async Task<bool> UpdatePostAsync(int id, UpdatePostDto updateDto, string userId)
        {
            var postEntity = await _postRepository.GetByIdAsync(id);

            if (postEntity == null) throw ExceptionManager.NotFound(nameof(Post), id.ToString());
            if (postEntity.UserId != userId) throw ExceptionManager.AccessDenied();
            postEntity.Content = updateDto.Content;

            return await _postRepository.UpdateAsync(postEntity);
        }

        public async Task<bool> DeletePostAsync(int id, string userId)
        {
            var post = await _postRepository.GetByIdAsync(id);

            if (post == null) throw ExceptionManager.NotFound(nameof(Post), id.ToString());

            if (post.UserId != userId) throw ExceptionManager.AccessDenied();

            return await _postRepository.DeleteAsync(id);
        }

        public async Task<bool> PostExists(int id)
        {
            return await _postRepository.GetByIdAsync(id) != null;
        }
    }
}
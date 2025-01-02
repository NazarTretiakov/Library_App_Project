using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class PostsGetterService : IPostsGetterService
    {
        private readonly IPostsRepository _postsRepository;
        public PostsGetterService(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _postsRepository.GetAllPosts();
        } 

        public async Task<Post> GetPostByPostId(string postId)
        {
            return await _postsRepository.GetPost(postId);
        }
    }
}

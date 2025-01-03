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

        public async Task<List<Post>> GetFilteredPosts(string searchFilter, string searchString)
        {
            List<Post> filteredPosts = null;

            switch (searchFilter)
            {
                case "all":
                    filteredPosts = await _postsRepository.GetFilteredPosts(p => p.Title.ToUpper().Contains(searchString.ToUpper()) || 
                                                                            p.User.NormalizedUserName.Contains(searchString.ToUpper()) ||
                                                                            p.Topics.Any(topic => topic.Topic.Name.ToUpper() == searchString.ToUpper()) ||
                                                                            p.Title.ToUpper().Contains(searchString.ToUpper()));
                break;

                case "username":
                    filteredPosts = await _postsRepository.GetFilteredPosts(p => p.Title.ToUpper().Contains(searchString.ToUpper()));
                break;

                case "topic":
                    filteredPosts = await _postsRepository.GetFilteredPosts(p => p.Topics.Any(topic => topic.Topic.Name.ToUpper() == searchString.ToUpper()));
                break;

                case "title":
                    filteredPosts = await _postsRepository.GetFilteredPosts(p => p.Title.ToUpper().Contains(searchString.ToUpper()));
                break;
            }

            return filteredPosts;
        }

        public async Task<Post> GetPostByPostId(string postId)
        {
            return await _postsRepository.GetPost(postId);
        }
    }
}

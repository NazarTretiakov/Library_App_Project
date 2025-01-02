using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Core.Services
{
    public class PostsCreatorService : IPostsCreatorService
    {
        private readonly IPostsRepository _postsRepository;
        private readonly ITopicsRepository _topicsRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public PostsCreatorService(IPostsRepository postsRepository, ITopicsRepository topicsRepository, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _postsRepository = postsRepository;
            _topicsRepository = topicsRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<Post> CreatePost(PostDTO postDTO)
        {
            string[] topicNames;
            Guid postId = Guid.NewGuid();
            Post post = new Post();

            if (postDTO.Topics.Contains(","))
            {
                topicNames = postDTO.Topics.Replace(" ", "").Split(",");
            }
            else
            {
                topicNames = postDTO.Topics.Split(" ");
            }

            List<PostTopic> topics = new List<PostTopic>();

            foreach (string topicName in topicNames)
            {
                Topic topicFromDb = await _topicsRepository.GetTopic(topicName);

                if (topicFromDb != null)
                {
                    topics.Add(new PostTopic()
                    {
                        PostTopicId = Guid.NewGuid(),
                        PostId = postId,
                        Post = post,
                        TopicId = topicFromDb.TopicId,
                        Topic = topicFromDb
                    });
                }
                else
                {
                    Topic newTopic = new Topic()
                    {
                        TopicId = Guid.NewGuid(),
                        Name = topicName
                    };

                    await _topicsRepository.AddTopic(newTopic);

                    topics.Add(new PostTopic()
                    {
                        PostTopicId = Guid.NewGuid(),
                        PostId = postId,
                        Post = post,
                        TopicId = newTopic.TopicId,
                        Topic = newTopic
                    });
                }
            }

            post = new Post()
            {
                PostId = postId,
                User = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User),
                Title = postDTO.Title,
                Topics = topics,
                Content = postDTO.Content,
                DateOfPublication = DateTime.Now
            };

            await _postsRepository.AddPost(post);

            return post;
        }
    }
}

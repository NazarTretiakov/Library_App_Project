using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Core.Services
{
    public class CommentsCreatorService : ICommentsCreatorService
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IPostsGetterService _postsGetterService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public CommentsCreatorService(ICommentsRepository commentsRepository, IPostsGetterService postsGetterService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _commentsRepository = commentsRepository;
            _postsGetterService = postsGetterService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<Comment> CreateComment(CreateCommentDTO createCommentDTO)
        {
            Comment newComment = new Comment()
            {
                CommentId = Guid.NewGuid(),
                User = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User),
                Post = await _postsGetterService.GetPostByPostId(createCommentDTO.PostId),
                Content = createCommentDTO.Content,
                DateOfPublication = DateTime.Now
            };

            await _commentsRepository.AddComment(newComment);

            return newComment;
        }
    }
}

using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class CommentsGetterService : ICommentsGetterService
    {
        private readonly ICommentsRepository _commentsRepository;

        public CommentsGetterService(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        public async Task<List<Comment>> GetUserComments(string userId)
        {
            return await _commentsRepository.GetCommentsByUser(userId);
        }

        public async Task<List<Comment>> GetUserFilteredComments(string userId, string searchFilter, string searchString)
        {
            List<Comment> filteredComments = null;

            switch (searchFilter)
            {
                case "all":
                    filteredComments = await _commentsRepository.GetFilteredCommentsByUser(userId, c => c.Post.Title.ToUpper().Contains(searchString.ToUpper()) ||
                                                                                                        c.Post.User.NormalizedUserName.Contains(searchString.ToUpper()) ||
                                                                                                        c.Post.Topics.Any(topic => topic.Topic.Name.ToUpper() == searchString.ToUpper()));
                break;

                case "username":
                    filteredComments = await _commentsRepository.GetFilteredCommentsByUser(userId, c => c.Post.User.NormalizedUserName.Contains(searchString.ToUpper()));
                break;

                case "topic":
                    filteredComments = await _commentsRepository.GetFilteredCommentsByUser(userId, c => c.Post.Topics.Any(topic => topic.Topic.Name.ToUpper().Contains(searchString.ToUpper())));
                    break;

                case "title":
                    filteredComments = await _commentsRepository.GetFilteredCommentsByUser(userId, c => c.Post.Title.ToUpper().Contains(searchString.ToUpper()));
                break;
            }

            return filteredComments;
        }
    }
}

using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Core.Services
{
    public class ToggleSaveService : IToggleSaveService
    {
        private readonly IPostsGetterService _postsGettersService;
        private readonly ISavesGetterService _savesGetterService;
        private readonly ISavesRemoverService _savesRemoverService;
        private readonly ISavesAdderService _savesAdderService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public ToggleSaveService(IPostsGetterService postsGetterService, ISavesGetterService savesGetterService, ISavesRemoverService savesRemoverService, ISavesAdderService savesAdderService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _postsGettersService = postsGetterService;
            _savesGetterService = savesGetterService;
            _savesRemoverService = savesRemoverService;
            _savesAdderService = savesAdderService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<bool> ToggleSave(string id)  //  TODO: finish logic related to saves of the books (that logic is commented)
        {
            User user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            Post post = await _postsGettersService.GetPostByPostId(id);
            //Book book = await _booksGetterService.getBookByBookId(id);

            if (post != null)
            {
                Save saveOfUser = await _savesGetterService.GetSaveByUserIdAndObjectId(user.Id.ToString(), post.PostId.ToString());

                if (saveOfUser != null)
                {
                    await _savesRemoverService.RemoveSave(saveOfUser);

                    return false;
                }
                else
                {
                    await _savesAdderService.AddPostSave(post, user);

                    return true;
                }
            }
            //else if (book != null)
            //{
            //    Save saveOfUser = await _savesGetterService.GetSaveByUserIdAndObjectId(user.Id.ToString(), book.bookId.ToString());

            //    if (saveOfUser != null)
            //    {
            //        await _savesRemoverService.RemoveSave(saveOfUser);

            //        return false;
            //    }
            //    else
            //    {
            //        await _savesAdderService.AddBookSave(book, user);

            //        return true;
            //    }
            //}
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}

using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Controllers
{
    [Authorize(Roles = "User")]
    public class ForumController : Controller
    {
        private readonly IPostsCreatorService _postsCreatorService;
        private readonly IPostsGetterService _postsGetterService;
        private readonly IToggleLikeService _toggleLikeService;
        private readonly ILikesGetterService _likesGetterService;
        private readonly IIsPostLikedService _isPostLikedService;
        private readonly IToggleSaveService _toggleSaveService;
        private readonly IIsPostSavedService _isPostSavedService;
        private readonly ICommentsCreatorService _commentsCreatorService;
        private readonly UserManager<User> _userManager;

        public ForumController(IPostsCreatorService postAdderService, IPostsGetterService postsGetterService, IToggleLikeService toggleLikeService, ILikesGetterService likesGetterService, IIsPostLikedService isPostLikedService, IToggleSaveService toggleSaveService, IIsPostSavedService isPostSavedService, ICommentsCreatorService commentsCreatorService, UserManager<User> userManager)
        {
            _postsCreatorService = postAdderService;
            _postsGetterService = postsGetterService;
            _toggleLikeService = toggleLikeService;
            _likesGetterService = likesGetterService;
            _isPostLikedService = isPostLikedService;
            _toggleSaveService = toggleSaveService;
            _isPostSavedService = isPostSavedService;
            _commentsCreatorService = commentsCreatorService;
            _userManager = userManager;
        }

        [Route("/forum")]
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, string searchFilter = "all")
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            ViewBag.SearchString = searchString;
            ViewBag.SearchFilter = searchFilter;

            List<Post> posts;

            if (searchString == null)
            {
                posts = await _postsGetterService.GetAllPosts();
            }
            else if (searchFilter != "all" && searchFilter != "username" && searchFilter != "topic" && searchFilter != "title")
            {
                return NotFound();  //TODO: create custom exception page for that type of situations (input searchString is not correct)
            }
            else
            {
                posts = await _postsGetterService.GetFilteredPosts(searchFilter, searchString);
            }

            posts = posts.OrderByDescending(p => p.DateOfPublication).ToList();  //TODO: find out why I can't use here async version of query methods

            return View(posts);  //TODO: create empty state of the page, for situation when posts with mentioned "searchString" weren't found
        }

        [Route("/forum/post")]
        public async Task<IActionResult> Post(string postId)  //TODO: create custom exception page for that type of situations (when the postId is not entered in the query string)
        {
            if (!Guid.TryParse(postId, out Guid result))
            {
                return NotFound();  //TODO: create custom exception page for that type of situations (input postId is not in the correct format, or postId is not present in the query string)
            }

            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            Post post = await _postsGetterService.GetPostByPostId(postId);

            if (post == null)
            {
                return NotFound();  //TODO: create custom exception page for that type of situations (when the post is not found in db)
            }

            List<Like> likesOfPost = await _likesGetterService.GetPostLikes(postId);

            ViewBag.IsLiked = await _isPostLikedService.IsPostLiked(postId);
            ViewBag.LikesCount = likesOfPost.Count;
            ViewBag.IsSaved = await _isPostSavedService.IsPostSaved(postId);

            return View(post); //TODO: create empty state of the page, for situation when posts has no comments
        }

        [Route("/forum/post/toggle-like")]
        [HttpPost]
        public async Task<IActionResult> ToggleLike([FromBody] ToggleLikeDTO toggleLikeDTO)
        {
            bool isPostLiked = await _toggleLikeService.ToggleLike(toggleLikeDTO.PostId);
            List<Like> likesOfPost = await _likesGetterService.GetPostLikes(toggleLikeDTO.PostId);
            Post post = await _postsGetterService.GetPostByPostId(toggleLikeDTO.PostId);

            ViewBag.IsLiked = isPostLiked;
            ViewBag.LikesCount = likesOfPost.Count;

            return PartialView("_Like", post);
        }

        [Route("/forum/post/toggle-post-save")]
        [HttpPost]
        public async Task<IActionResult> TogglePostSave([FromBody] TogglePostSaveDTO toggleSaveDTO)
        {
            bool isPostSaved = await _toggleSaveService.ToggleSave(toggleSaveDTO.PostId);
            Post post = await _postsGetterService.GetPostByPostId(toggleSaveDTO.PostId);

            ViewBag.IsSaved = isPostSaved;

            return PartialView("_Save", post);
        }

        [Route("/forum/post/create-comment")]
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDTO createCommentDTO)
        {
            await _commentsCreatorService.CreateComment(createCommentDTO);

            Post currentPost = await _postsGetterService.GetPostByPostId(createCommentDTO.PostId);

            return PartialView("_Comments", currentPost);
        }

        [Route("/forum/create-post")]
        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }

        [Route("/forum/create-post")]
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostDTO postDTO)
        {
            if (ModelState.IsValid == false)
            {
                return View(postDTO);
            }

            Post createdPost = await _postsCreatorService.CreatePost(postDTO);
            
            return RedirectToAction(nameof(ForumController.Post), "Forum", new {postId = createdPost.PostId});
        }
    }
}

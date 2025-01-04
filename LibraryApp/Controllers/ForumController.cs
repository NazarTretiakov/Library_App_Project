﻿using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Controllers
{
    public class ForumController : Controller
    {
        private readonly IPostsCreatorService _postsCreatorService;
        private readonly IPostsGetterService _postsGetterService;
        private readonly IToggleLikeService _toggleLikeService;
        private readonly ILikesGetterService _likesGetterService;
        private readonly IIsPostLikedService _isPostLikedService;

        public ForumController(IPostsCreatorService postAdderService, IPostsGetterService postsGetterService, IToggleLikeService toggleLikeService, ILikesGetterService likesGetterService, IIsPostLikedService isPostLikedService)
        {
            _postsCreatorService = postAdderService;
            _postsGetterService = postsGetterService;
            _toggleLikeService = toggleLikeService;
            _likesGetterService = likesGetterService;
            _isPostLikedService = isPostLikedService;
        }

        [Route("/forum")]
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, string searchFilter = "all")
        {
            ViewBag.SearchString = searchString;
            ViewBag.SearchFilter = searchFilter;

            List<Post> posts;

            if (searchString == null)
            {
                posts = await _postsGetterService.GetAllPosts();
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
            Post post = await _postsGetterService.GetPostByPostId(postId);
            List<Like> likesOfPost = await _likesGetterService.GetPostLikes(postId);

            ViewBag.IsLiked = await _isPostLikedService.IsPostLiked(postId);
            ViewBag.LikesCount = likesOfPost.Count;

            if (post == null)
            {
                return NotFound();  //TODO: create custom exception page for that type of situations (when the post is not found in db)
            }

            return View(post);
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

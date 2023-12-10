using System;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using perpark_api.DevelopmentClasses;
using perpark_api.Models;
using perpark_api.Models.Entities;

namespace perpark_api.Controllers;

[ApiController]
[Route("[controller]")]
public class SocialMediaController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<SocialMediaController> _logger;
    Models.Context _db = new Models.Context();

    public SocialMediaController(ILogger<SocialMediaController> logger,IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }
    [HttpPost("AddPost")]
    public IActionResult AddPost(Post myPost)
    {
        try
        {
            _db.Posts.Add(myPost);
            _db.SaveChanges();
            PostDetailVM vm = new PostDetailVM();
            vm.datetime = myPost.datetime;
            var pp = _db.Users.FirstOrDefault(x => x.UserId == myPost.UserId);
            vm.OwnerPPUrl = pp.ProfilePictureUrl;
            vm.PostId = myPost.PostId;
            vm.PostText = myPost.PostText;
            vm.UserId = myPost.UserId;
            vm.UserName = pp.Username;
            return Ok(vm);
        }
        catch(Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "AddPost";
            log.UserId = myPost.UserId;
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
    }
    [HttpGet("DeletePost")]
    public IActionResult DeletePost(int postId)
    {
        try
        {
            var post = _db.Posts.FirstOrDefault(x => x.PostId == postId);
            _db.Posts.Remove(post);
            _db.SaveChanges();
            return Ok(post);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "DeletePost";
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
    }
    [HttpPost("MakeComment")]
    public IActionResult MakeComment(Comment comment)
    {
        try
        {
            _db.Comments.Add(comment);
            _db.SaveChanges();
            return Ok(comment);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "MakeComment";
            log.UserId = comment.UserId;
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        
    }
    [HttpGet("DeleteComment")]
    public IActionResult DeleteComment(int commentId)
    {
        try
        {
            var comment = _db.Comments.FirstOrDefault(x => x.CommentId == commentId);
            _db.Comments.Remove(comment);
            _db.SaveChanges();
            return Ok();
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "DeleteComment";

            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }

    }
    [HttpPost("RandomPosts")]
    public async Task<IActionResult> RandomPosts(Search search,int userId)
    {
        try
        {
            
            
            DateTime yesterday = DateTime.Now.AddHours(-18);
            var Posts =  _db.Posts.Where(x => x.datetime > yesterday).Skip(((int)search.PageNumber - 1) * (int)search.PageSize)
                        .Take((int)search.PageSize)
                        .ToList();

            List<PostDetailVM> vm = new List<PostDetailVM>();
            foreach (var item in Posts)
            {
                PostDetailVM post = new PostDetailVM();
                post.PostText = item.PostText;
                var likeCount = _db.Likes.Where(x => x.PostId == item.PostId).Count();
                var commentCount = _db.Comments.Where(x => x.PostId == item.PostId).Count();
                post.CommentCount = commentCount;
                post.PostLikeCount = likeCount;
                post.PostImageUrl = item.PostImageUrl;
                post.PostId = item.PostId;
                post.datetime = item.datetime;
                var user = _db.Users.FirstOrDefault(x => x.UserId == item.UserId);
                var ownerUrl = user.ProfilePictureUrl;
                post.OwnerPPUrl = ownerUrl;
                var isLikeOwner = _db.Likes.FirstOrDefault(x => x.PostId == item.PostId && x.UserId == item.UserId);
                if (isLikeOwner != null)
                {
                    post.IsLikeOwner = true;
                }
                else
                {
                    post.IsLikeOwner = false;
                }
                post.UserId = user.UserId;
                post.UserName = user.Username;
                vm.Add(post);
            }
            
            return Ok(vm);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "RandomPosts";
            log.UserId = userId;
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        
    }
    [HttpGet("UnLikepost")]
    public IActionResult UnLikePost(int likeFromId,int PostId)
    {
        try
        {
            var likes = _db.Likes.FirstOrDefault(x => x.PostId == PostId && x.UserId == likeFromId);
            _db.Remove(likes);
            _db.SaveChanges();
            return Ok(likes);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "UnLikePost";
            log.UserId = likeFromId;
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
    }
    [HttpGet("Likepost")]
    public IActionResult LikePost(int likeFromId, int PostId)
    {
        try
        {
            Like likes = new Like();
            likes.PostId = PostId;
            likes.UserId = likeFromId;
            _db.Likes.Add(likes);
            _db.SaveChanges();
            return Ok(likes);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "LikePost";
            log.UserId = likeFromId;
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
    }
    [HttpGet("RandomPets")]
    public IActionResult RandomPets(int sessionUserId)
    {
        try
        {
            
            List<UserAnimalVM> vmList = new List<UserAnimalVM>();
            var animals = _db.Animals.Where(x => x.UserId != sessionUserId && x.AnimalImageUrl != null && x.AnimalImageUrl != "").ToList();

            if (animals.Count > 14)
            {
                animals = animals.TakeLast(15).ToList();
            }


            var users = _db.Users.ToList();
            var sickness = _db.Sickess.ToList();
            var vacs = _db.Vaccines.ToList();
            foreach (var animal in animals)
            {
                UserAnimalVM vm = new UserAnimalVM();
                var subUser = users.FirstOrDefault(x => x.UserId == animal.UserId);
                vm.user = subUser;
                vm.Animal = animal;

                vm.Sicknesses = sickness.Where(x => x.AnimalId == animal.AnimalId).ToList();
                vm.Vaccines = vacs.Where(x => x.AnimalId == animal.AnimalId).ToList();
                vmList.Add(vm);
            }
            return Ok(vmList);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "RandomPets";
            log.UserId = sessionUserId;
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        
        
    }
    [HttpGet("PostDetail")]
    public IActionResult PostDetail(int postId,int userId)
    {
        try
        {
            var post = _db.Posts.FirstOrDefault(x => x.PostId == postId);
            var ownerPostUserName = _db.Users.FirstOrDefault(x => x.UserId == post.UserId).Username;
            var user = _db.Users.FirstOrDefault(x => x.UserId == userId);
            PostDetailVM vm = new PostDetailVM();
            vm.PostId = post.PostId;
            vm.UserId = post.UserId;
            vm.UserName = ownerPostUserName;
            vm.OwnerPPUrl = user.ProfilePictureUrl;
            string ownerpp = _db.Users.FirstOrDefault(x => x.UserId == post.UserId).ProfilePictureUrl;
            vm.OwnerPPUrl = ownerpp;
            vm.PostImageUrl = post.PostImageUrl;
            vm.PostText = post.PostText;
            vm.datetime = post.datetime;
            int islikeow = _db.Likes.Where(x => x.UserId == userId && x.PostId == postId).ToList().Count();
            if (islikeow != 0 && islikeow != null)
            {
                vm.IsLikeOwner = true;
            }
            else
            {
                vm.IsLikeOwner = false;
            }
            int likeCount = _db.Likes.Where(x => x.PostId == postId).Count();
            vm.PostLikeCount = likeCount;
            var postComment = _db.Comments.Where(x => x.PostId == postId).ToList();
            vm.CommentCount = postComment.ToList().Count();
            List<CommentVM> commentvms = new List<CommentVM>();
            foreach (var item in postComment)
            {
                CommentVM cm = new CommentVM();
                var userSub = _db.Users.FirstOrDefault(x => x.UserId == item.UserId);
                cm.UserName=userSub.Username;
                cm.UserPP=userSub.ProfilePictureUrl;
                cm.UserId = item.UserId;
                cm.PostId = item.PostId;
                cm.datetime = item.datetime;
                cm.Content = item.Content;
                cm.CommentId = item.CommentId;
                commentvms.Add(cm);
            }
            vm.Comments = commentvms;
            return Ok(vm);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "PostDetail";
            log.UserId = userId;
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }   
    }
    [HttpPost("UserPosts")]
    public async Task<IActionResult> UserPosts(int userId,Search search)
    {
        try
        {
            var myPosts = _db.Posts.Where(x => x.UserId == userId).ToList().OrderByDescending(x => x.datetime).Skip(((int)search.PageNumber - 1) * (int)search.PageSize)
                    .Take((int)search.PageSize);
                    
            /*List<PostDetailVM> myPostsx = new List<PostDetailVM>();

            foreach (var item in myPosts)
            {
                PostDetailVM vm = new PostDetailVM();
                vm.PostText = item.PostText;
                int likeCount = _db.Likes.Where(x => x.PostId == item.PostId).Count();
                vm.PostLikeCount = likeCount;
                vm.PostImageUrl = item.PostImageUrl;
                string ownerpp = _db.Users.FirstOrDefault(x => x.UserId == item.UserId).ProfilePictureUrl;
                vm.OwnerPPUrl = ownerpp;
                
                var postComment = _db.Comments.Where(x => x.PostId == item.PostId).ToList();
            }*/

            return Ok(myPosts);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "UserPosts";
            log.UserId = userId;
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        
    }
}
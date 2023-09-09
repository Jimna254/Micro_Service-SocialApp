using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialApp_Posts.Models;
using SocialApp_Posts.Models.DTOs;
using SocialApp_Posts.Services.IServices;
using System.Data;

namespace SocialApp_Posts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly ResponseDTO _response;

        public PostController(IPostService postService, IMapper mapper )
        {
            _mapper = mapper;
            _postService = postService;
            _response = new ResponseDTO();

        }

        //Endpoint to create Post

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreatePost(PostRequestDTO postRequestDto)
        {
            var newPost = _mapper.Map<Post>(postRequestDto);
            var response = await _postService.CreatePostAsync(newPost);
            if (response != null)
            {
                _response.IsSuccess = true;
                _response.Message = response;
                return Ok(_response);
            }
            _response.IsSuccess = false;
            _response.Message = "An Error Occured";
            return BadRequest(_response);
        }

        //Endpoint to get All  Posts

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            if (posts != null)
            {
                _response.Message = "";
                _response.IsSuccess = true;
                _response.Data = posts;
                return Ok(_response);
            }
            _response.IsSuccess = false;
            _response.Message = "An Error Occured";
            return BadRequest(_response);
        }

        // Endpoint to Get post by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetPostById(Guid id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post != null)
            {
                _response.Message = "";
                _response.IsSuccess = true;
                _response.Data = post;
                return Ok(_response);
            }
            _response.IsSuccess = false;
            _response.Message = "An Error Occured";
            return BadRequest(_response);
        }
        //Deletion

        [HttpDelete("{id}")]
       
        public async Task<ActionResult<ResponseDTO>> DeletePost(Guid id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post != null)
            {
                var response = await _postService.DeletePostAsync(post);
                if (response != null)
                {
                    _response.IsSuccess = true;
                    _response.Message = "Post Deleted Succesfully";
                    return Ok(_response);
                }
                _response.IsSuccess = false;
                _response.Message = "An Error Occured";
                return BadRequest(_response);
            }
            _response.IsSuccess = false;
            _response.Message = "Post Does not Exist";
            return BadRequest(_response);

        }
        //Fetching Posts by User ID

        [HttpGet("userId")]
        public async Task<ActionResult<ResponseDTO>> GetAllPostsByUserId(Guid userId)
        {
            var posts = await _postService.GetPostsByUserIdAsync(userId);
            if (posts != null)
            {
                _response.Message = "";
                _response.IsSuccess = true;
                _response.Data = posts;
                return Ok(_response);
            }
            _response.IsSuccess = false;
            _response.Message = "An Error Occured";
            return BadRequest(_response);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDTO>> UpdatePost(Guid id, PostRequestDTO postRequestDto)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Post Does not Exist";
                return BadRequest(_response);
            }
            var postToUpdate = _mapper.Map(postRequestDto, post);
            var response = await _postService.UpdatePostAsync(postToUpdate);
            if (response != null)
            {
                _response.IsSuccess = true;
                _response.Message = "Post Updated Successfuly";
                return Ok(_response);
            }
            _response.IsSuccess = false;
            _response.Message = "An Error Occured";
            return BadRequest(_response);
        }
    }
}

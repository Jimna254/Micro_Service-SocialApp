﻿using SocialApp_Posts.Data;
using SocialApp_Posts.Models.DTOs;
using SocialApp_Posts.Services.IServices;
using Newtonsoft.Json;
using SocialApp_Posts.Models;

namespace SocialApp_Posts.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _clientFactory;
        public CommentService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<CommentDTO>> GetCommentsAsync(string PostID)
        {
            var client = _clientFactory.CreateClient("Comment");
            var response = await client.GetAsync($"/api/Comment/GetAllCommentsByPostId/{PostID}");
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDTO>(content);

            if (responseDto.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<CommentDTO>>(Convert.ToString(responseDto)); 
            }
            return new List<CommentDTO>();
            
        }
    }
}

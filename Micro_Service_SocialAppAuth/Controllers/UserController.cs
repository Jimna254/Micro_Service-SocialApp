using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialAppAuthentication.Models;
using SocialAppAuthentication.Services.IServices;

namespace SocialAppAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private IUserServices _iuserService;
        private readonly ResponseDTO _response;

        public UserController(IUserServices userServices)
        {
            _iuserService = userServices;
            // don't inject the DTO

            _response = new ResponseDTO();

        }
        [HttpPost("register")]
        public async Task<ActionResult<ResponseDTO>> AddUSer(RegisterRequestDTO registerRequestDto)
        {
            var errorMessage = await _iuserService.RegisterUser(registerRequestDto);
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                //error
                _response.IsSuccess = false;
                _response.Message = errorMessage;

                return BadRequest(_response);
            }

            return Ok(_response);


        }
        [HttpPost("Login")]
        public async Task<ActionResult<ResponseDTO>> LoginUser(LoginRequestDTO loginRequestDto)
        {
            var response = await _iuserService.Login(loginRequestDto);

            if (response == null)
            {
                // Error
                var errorResponse = new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Invalid Credentials"
                };

                return BadRequest(errorResponse);
            }

            // Successful login
            return Ok(response);
        }

        [HttpPost("AssignUserRole")]
        public async Task<ActionResult<ResponseDTO>> AssignRole(RegisterRequestDTO registerRequestDto)
        {
            var response = await _iuserService.AssignUserRole(registerRequestDto.Email, registerRequestDto.Role);
            if (!response)
            {
                //error
                _response.IsSuccess = false;
                _response.Message = "Error Occured";

                return BadRequest(_response);
            }
            _response.Result = response;
            return Ok(_response);
        }

    }
}
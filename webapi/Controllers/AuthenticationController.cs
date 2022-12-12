using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Commons;
using webapi.DataLayer.Repositories;
using webapi.Models.BasicModel;
using webapi.Models.RequestModel;
using webapi.Models.ResponseModel;

namespace webapi.Controllers;
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IUserRepository userRepository;
    private readonly IUserSessionRepository userSessionRepository;

    public AuthenticationController(
        IUserRepository userRepository,
        IUserSessionRepository userSessionRepository
        )
    {
        this.userRepository = userRepository;
        this.userSessionRepository = userSessionRepository;
    }
    [Route("api/authenication/Login")]
    [HttpPost]
    public IActionResult Login([FromBody] LoginRequestModel loginRequest)
    {
        if (string.IsNullOrEmpty(loginRequest.UserName) || string.IsNullOrEmpty(loginRequest.Password))
        {
            return BadRequest(ApiResponseCode.LOGIN_ERROR);
        }

        var user = userRepository.GetByName(loginRequest);

        if(user is null)
        {
            return BadRequest(ApiResponseCode.LOGIN_ERROR);
        }

        bool IsNeedToGenToken = true;
        LoginResponseModel loginResponse = new();
        if (IsNeedToGenToken)
        {
            var session = userSessionRepository.GetById(user.Id);
            if (session is null)
            {
                session = UserSession.CreateSessionTokenForUser(user.Id);
                userSessionRepository.Insert(session);
            }
            else
            {
                session.GenerateNewSessionToken();
                userSessionRepository.Update(session);
            }
            loginResponse.UserId = user.Id;
            loginResponse.UserName = user.UserName;
            loginResponse.SessionToken = session.SessionToken.ToString();
        }
        return Ok(loginResponse);
    }
}


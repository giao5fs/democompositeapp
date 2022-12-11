using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Commons;
using webapi.DataLayer.Repositories;
using webapi.Models.ApiModel;
using webapi.Models.BasicModel;

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
    public IActionResult Login([FromBody] LoginModel loginModel)
    {
        if (string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
        {
            return BadRequest(ApiResponseCode.LOGIN_ERROR);
        }


        var user = (from u in userRepository.GetAll()
                    where u.UserName == loginModel.UserName
                    && u.Password == loginModel.Password
                    select u).FirstOrDefault();

        if(user is null)
        {
            return BadRequest(ApiResponseCode.LOGIN_ERROR);
        }

        bool IsNeedToGenToken = true;
        UserLogin userLogin = new UserLogin();
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
            userLogin.UserId = user.Id;
            userLogin.UserName = user.UserName;
            userLogin.SessionToken = session.SessionToken.ToString();
        }
        return Ok(userLogin);
    }
}


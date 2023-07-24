using LivrariaFuturo.Authentication.Core.Domain;
using LivrariaFuturo.Authorization.Core.Domain;
using LivrariaFuturo.API.Models.Requests;
using LivrariaFuturo.Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LivrariaFuturo.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authenticationService;
        private readonly IUserService _userService;

        public AuthenticationController(IAuthService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userLogin)
        {
            var user = await this._authenticationService.Authenticate(userLogin.email, userLogin.password);

            if (user != null)
            {   
                var token = await this._authenticationService.GenerateJwtToken(user);
                var response = new UserLoginResponse(user, token);

                return new ApiResult(new Saida((int)HttpStatusCode.OK, true, "", response));
            }

            return new ApiResult(new NotFoundApiResponse("Acesso Negado"));
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationRequest userCreation)
        {
            var success = await this._userService.Save(userCreation.email, userCreation.name, userCreation.password);

            if (success) return new ApiResult(new Saida((int)HttpStatusCode.OK, true, "Conta criada com sucesso", success));

            return new ApiResult(new BadRequestApiResponse("Erro ao criar a conta"));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using OnePlan.Business.Dto.Auth;
using OnePlan.Core.Services.Interfaces;
using OnePlan.Business.ResponseModels;
using OnePlan.Business.ViewModels;

namespace OnePlan.Web.Controllers;

[Route("api/")]
public class AuthController : BaseController<IAuthService>
{
    public AuthController(IAuthService service) : base(service) {}

    [HttpPost("sign-in")]
    public async Task<ActionResult<Response<LoginViewModel>>> Login([FromBody] LoginDto loginDto)
    {
        LoginViewModel loginResult = await _service.Login(loginDto);
        return ResponseModel(loginResult);
    }
}

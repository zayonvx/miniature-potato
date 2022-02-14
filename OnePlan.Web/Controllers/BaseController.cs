using Microsoft.AspNetCore.Mvc;
using OnePlan.Business.Consts;
using OnePlan.Business.ViewModels;

namespace OnePlan.Web.Controllers;

public class BaseController<TService> : Controller
{
    protected TService _service;

    protected BaseController(TService service)
    {
        _service = service;
    }

    protected UserTokenInfo UserInfo => new()
    {
        UserId = int.Parse(User.FindFirst(CustomClaims.UserId).Value),
        Email = User.FindFirst(CustomClaims.Email).Value,
        Role = User.FindFirst(CustomClaims.Role).Value
    };

    protected ActionResult<Response<T>> ResponseModel<T>(T Result)
    {
        
    }
}

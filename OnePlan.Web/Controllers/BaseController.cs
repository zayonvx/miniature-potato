using Microsoft.AspNetCore.Mvc;
using OnePlan.Business.Consts;
using OnePlan.Business.ViewModels;
using OnePlan.Business.ResponseModels;

namespace OnePlan.Web.Controllers;

public class BaseController<TService> : Controller
{
    protected TService _service;
    protected BaseController(TService service)
    {
        _service = service;
    }

    protected UserTokenViewModel UserInfo => new()
    {
        UserId = int.Parse(User.FindFirst(CustomClaims.UserId).Value),
        Email = User.FindFirst(CustomClaims.Email).Value,
        Role = User.FindFirst(CustomClaims.Role).Value
    };

    protected ActionResult<Response<T>> ResponseModel<T>(T result)
    {
        return Json(new Response<T>(result));
    }
}

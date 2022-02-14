using Microsoft.AspNetCore.Mvc;

namespace OnePlan.Web.Controllers;

[Route(api/)]
public class AuthController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}

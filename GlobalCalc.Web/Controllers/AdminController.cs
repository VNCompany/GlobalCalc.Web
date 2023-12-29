using Microsoft.AspNetCore.Mvc;

using GlobalCalc.Web.Services;
using GlobalCalc.Web.Attributes;

namespace GlobalCalc.Web.Controllers;

public class AdminController : Controller
{
    private readonly IConfiguration _config;
    private readonly AuthorizationService _auth;

    public AdminController(IConfiguration config, AuthorizationService auth)
    {
        _config = config;
        _auth = auth;
    }

    [AdminAuthorize]
    public IActionResult Index()
    {
        return Ok("Hello World!");
    }

    public string Hi() => "Hello world!";

    public IActionResult Login(string? login, string? password)
    {
        if (login == null || password == null)
            return View();

        if (!_auth.Login(login, password, Response.Cookies))
        {
            ViewData["Login"] = false;
            return View();
        }

        return RedirectToAction(nameof(Index));
    }
}

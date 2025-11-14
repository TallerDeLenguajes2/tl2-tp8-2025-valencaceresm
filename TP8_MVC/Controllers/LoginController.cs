using Microsoft.AspNetCore.Mvc;
using TP8_MVC.Interfaces;
using TP8_MVC.ViewModels;

namespace TP8_MVC.Controllers;

public class LoginController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public LoginController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var loggedIn = _authenticationService.Login(model.Username, model.Password);
        if (!loggedIn)
        {
            model.ErrorMessage = "Usuario o contraseña incorrectos.";
            return View(model);
        }

        // Redirigir a la página principal o a Presupuestos por defecto
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        _authenticationService.Logout();
        return RedirectToAction("Index");
    }
}

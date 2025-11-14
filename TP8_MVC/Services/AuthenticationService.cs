using System;
using Microsoft.AspNetCore.Http;
using TP8_MVC.Interfaces;

namespace TP8_MVC.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession GetSession()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null)
        {
            throw new InvalidOperationException("HttpContext no está disponible.");
        }

        return context.Session;
    }

    public bool Login(string username, string password)
    {
        var user = _userRepository.GetUser(username, password);
        if (user == null)
        {
            return false;
        }

        var session = GetSession();
        session.SetString("IsAuthenticated", "true");
        session.SetString("User", user.User);
        session.SetString("UserNombre", user.Nombre);
        session.SetString("Rol", user.Rol);

        return true;
    }

    public void Logout()
    {
        var session = GetSession();
        session.Clear();
    }

    public bool IsAuthenticated()
    {
        var session = GetSession();
        return session.GetString("IsAuthenticated") == "true";
    }

    public bool HasAccessLevel(string requiredAccessLevel)
    {
        var session = GetSession();
        var currentRole = session.GetString("Rol");
        return string.Equals(currentRole, requiredAccessLevel, StringComparison.OrdinalIgnoreCase);
    }
}

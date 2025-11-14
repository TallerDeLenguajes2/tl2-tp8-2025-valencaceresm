namespace TP8_MVC.Interfaces;

public interface IAuthenticationService
{
    bool Login(string username, string password);
    void Logout();

    /// <summary>
    /// Verifica si el usuario actual tiene una sesión activa.
    /// </summary>
    bool IsAuthenticated();

    /// <summary>
    /// Verifica si el usuario actual tiene el rol requerido (ej: "Administrador").
    /// </summary>
    bool HasAccessLevel(string requiredAccessLevel);
}

using Microsoft.Data.Sqlite;
using TP8_MVC.Interfaces;
using TP8_MVC.Models;

namespace TP8_MVC.Repositories;

public class UsuarioRepository : IUserRepository
{
    private readonly string _connectionString;

    public UsuarioRepository()
    {
        _connectionString = "Data Source=DB/tienda.db;Cache=Shared";
    }

    public Usuario? GetUser(string username, string password)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = @"SELECT Id, Nombre, User, Pass, Rol
                               FROM Usuarios
                               WHERE User = @username AND Pass = @password";

        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@password", password);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        return new Usuario
        {
            Id = reader.GetInt32(0),
            Nombre = reader.GetString(1),
            User = reader.GetString(2),
            Pass = reader.GetString(3),
            Rol = reader.GetString(4)
        };
    }
}

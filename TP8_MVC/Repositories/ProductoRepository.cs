using Microsoft.Data.Sqlite;
using TP8_MVC.Interfaces;
using TP8_MVC.Models;

namespace TP8_MVC.Repositories;

public class ProductoRepository : IProductoRepository
{
        private readonly string _connectionString;

        public ProductoRepository()
        {
            _connectionString = "Data Source=DB/tienda.db;Cache=Shared";
        }

        public List<Producto> GetAll()
        {
            var productos = new List<Producto>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT idProducto, Descripcion, Cantidad, Precio FROM Productos";
                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add(new Producto
                        {
                            IdProducto = reader.GetInt32(0),
                            Descripcion = reader.GetString(1),
                            Cantidad = reader.GetInt32(2),
                            Precio = reader.GetDecimal(3)
                        });
                    }
                }
            }
            return productos;
        }

        public Producto? GetById(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT idProducto, Descripcion, Cantidad, Precio FROM Productos WHERE idProducto = @id";
                command.Parameters.AddWithValue("@id", id);
                
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Producto
                        {
                            IdProducto = reader.GetInt32(0),
                            Descripcion = reader.GetString(1),
                            Cantidad = reader.GetInt32(2),
                            Precio = reader.GetDecimal(3)
                        };
                    }
                }
            }
            return null;
        }

        public void Create(Producto producto)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Productos (Descripcion, Cantidad, Precio) 
                    VALUES (@descripcion, @cantidad, @precio)";
                command.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@cantidad", producto.Cantidad);
                command.Parameters.AddWithValue("@precio", producto.Precio);
                command.ExecuteNonQuery();
            }
        }

        public void Update(Producto producto)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Productos 
                    SET Descripcion = @descripcion, Cantidad = @cantidad, Precio = @precio 
                    WHERE idProducto = @id";
                command.Parameters.AddWithValue("@id", producto.IdProducto);
                command.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@cantidad", producto.Cantidad);
                command.Parameters.AddWithValue("@precio", producto.Precio);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Productos WHERE idProducto = @id";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
    }

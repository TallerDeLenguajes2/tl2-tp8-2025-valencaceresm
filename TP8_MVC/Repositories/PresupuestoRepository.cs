using Microsoft.Data.Sqlite;
using TP8_MVC.Models;

namespace TP8_MVC.Repositories
{
    public class PresupuestoRepository
    {
        private readonly string _connectionString;

        public PresupuestoRepository()
        {
            _connectionString = "Data Source=DB/tienda.db;Cache=Shared";
        }

        public List<Presupuesto> GetAll()
        {
            var presupuestos = new List<Presupuesto>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT idPresupuesto, NombreDestinatario FROM Presupuestos";
                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        presupuestos.Add(new Presupuesto
                        {
                            IdPresupuesto = reader.GetInt32(0),
                            NombreDestinatario = reader.GetString(1)
                        });
                    }
                }
            }
            return presupuestos;
        }

        public Presupuesto? GetById(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                
                // Obtener presupuesto
                var command = connection.CreateCommand();
                command.CommandText = "SELECT idPresupuesto, NombreDestinatario FROM Presupuestos WHERE idPresupuesto = @id";
                command.Parameters.AddWithValue("@id", id);
                
                Presupuesto? presupuesto = null;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        presupuesto = new Presupuesto
                        {
                            IdPresupuesto = reader.GetInt32(0),
                            NombreDestinatario = reader.GetString(1)
                        };
                    }
                }

                if (presupuesto != null)
                {
                    // Obtener productos asociados
                    var cmdProductos = connection.CreateCommand();
                    cmdProductos.CommandText = @"
                        SELECT p.idProducto, p.Descripcion, pd.Cantidad, p.Precio 
                        FROM PresupuestosDetalle pd
                        INNER JOIN Productos p ON pd.idProducto = p.idProducto
                        WHERE pd.idPresupuesto = @id";
                    cmdProductos.Parameters.AddWithValue("@id", id);
                    
                    using (var readerProductos = cmdProductos.ExecuteReader())
                    {
                        while (readerProductos.Read())
                        {
                            presupuesto.Productos.Add(new Producto
                            {
                                IdProducto = readerProductos.GetInt32(0),
                                Descripcion = readerProductos.GetString(1),
                                Cantidad = readerProductos.GetInt32(2),
                                Precio = readerProductos.GetDecimal(3)
                            });
                        }
                    }
                }
                
                return presupuesto;
            }
        }

        public void Create(Presupuesto presupuesto)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) 
                    VALUES (@nombre, @fecha);
                    SELECT last_insert_rowid();";
                command.Parameters.AddWithValue("@nombre", presupuesto.NombreDestinatario);
                command.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                
                var idPresupuesto = Convert.ToInt32(command.ExecuteScalar());
                presupuesto.IdPresupuesto = idPresupuesto;
            }
        }

        public void Update(Presupuesto presupuesto)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Presupuestos 
                    SET NombreDestinatario = @nombre 
                    WHERE idPresupuesto = @id";
                command.Parameters.AddWithValue("@id", presupuesto.IdPresupuesto);
                command.Parameters.AddWithValue("@nombre", presupuesto.NombreDestinatario);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                
                // Primero eliminar detalles
                var cmdDetalle = connection.CreateCommand();
                cmdDetalle.CommandText = "DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @id";
                cmdDetalle.Parameters.AddWithValue("@id", id);
                cmdDetalle.ExecuteNonQuery();
                
                // Luego eliminar presupuesto
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Presupuestos WHERE idPresupuesto = @id";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        public void AddProducto(int idPresupuesto, int idProducto, int cantidad)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) 
                    VALUES (@idPresupuesto, @idProducto, @cantidad)";
                command.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
                command.Parameters.AddWithValue("@idProducto", idProducto);
                command.Parameters.AddWithValue("@cantidad", cantidad);
                command.ExecuteNonQuery();
            }
        }

        public void RemoveProducto(int idPresupuesto, int idProducto)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    DELETE FROM PresupuestosDetalle 
                    WHERE idPresupuesto = @idPresupuesto AND idProducto = @idProducto";
                command.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
                command.Parameters.AddWithValue("@idProducto", idProducto);
                command.ExecuteNonQuery();
            }
        }
    }
}

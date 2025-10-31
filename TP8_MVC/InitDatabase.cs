using Microsoft.Data.Sqlite;

namespace TP8_MVC
{
    public class InitDatabase
    {
        public static void Initialize()
        {
            var connectionString = "Data Source=DB/tienda.db;Cache=Shared";
            
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                
                var command = connection.CreateCommand();
                command.CommandText = @"
                    -- Crear tabla Productos
                    CREATE TABLE IF NOT EXISTS Productos (
                        idProducto INTEGER PRIMARY KEY AUTOINCREMENT,
                        Descripcion TEXT NOT NULL,
                        Cantidad INTEGER NOT NULL,
                        Precio REAL NOT NULL
                    );

                    -- Crear tabla Presupuestos
                    CREATE TABLE IF NOT EXISTS Presupuestos (
                        idPresupuesto INTEGER PRIMARY KEY AUTOINCREMENT,
                        NombreDestinatario TEXT NOT NULL,
                        FechaCreacion TEXT NOT NULL
                    );

                    -- Crear tabla PresupuestosDetalle
                    CREATE TABLE IF NOT EXISTS PresupuestosDetalle (
                        idPresupuestoDetalle INTEGER PRIMARY KEY AUTOINCREMENT,
                        idPresupuesto INTEGER NOT NULL,
                        idProducto INTEGER NOT NULL,
                        Cantidad INTEGER NOT NULL,
                        FOREIGN KEY (idPresupuesto) REFERENCES Presupuestos(idPresupuesto),
                        FOREIGN KEY (idProducto) REFERENCES Productos(idProducto)
                    );
                ";
                command.ExecuteNonQuery();

                // Verificar si ya hay datos
                var checkCommand = connection.CreateCommand();
                checkCommand.CommandText = "SELECT COUNT(*) FROM Productos";
                var count = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (count == 0)
                {
                    // Insertar datos de ejemplo
                    var insertCommand = connection.CreateCommand();
                    insertCommand.CommandText = @"
                        INSERT INTO Productos (Descripcion, Cantidad, Precio) VALUES
                        ('Laptop Dell', 10, 850.00),
                        ('Mouse Logitech', 50, 25.50),
                        ('Teclado Mecánico', 30, 75.00),
                        ('Monitor Samsung 24', 15, 320.00),
                        ('Auriculares Sony', 40, 45.99);

                        INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES
                        ('Juan Pérez', '2025-10-01'),
                        ('María García', '2025-10-15'),
                        ('Carlos López', '2025-10-20');

                        INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES
                        (1, 1, 2),
                        (1, 2, 5),
                        (2, 3, 1),
                        (2, 4, 2),
                        (3, 5, 3);
                    ";
                    insertCommand.ExecuteNonQuery();
                }
            }
        }
    }
}

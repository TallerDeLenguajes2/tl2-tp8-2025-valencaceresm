# TP8 - ASP.NET Core MVC - Gestión de Productos y Presupuestos

## Descripción
Aplicación web ASP.NET Core MVC para la gestión de productos y presupuestos con base de datos SQLite.

## Características
- **CRUD completo de Productos**: Crear, listar, editar y eliminar productos
- **CRUD completo de Presupuestos**: Crear, listar, editar y eliminar presupuestos
- **Vista de Detalles**: Visualizar presupuestos con sus productos asociados y monto total
- **Bootstrap**: Interfaz moderna y responsiva
- **SQLite**: Base de datos ligera y portable

## Estructura del Proyecto
```
TP8_MVC/
├── Controllers/
│   ├── ProductosController.cs
│   └── PresupuestosController.cs
├── Models/
│   ├── Producto.cs
│   └── Presupuesto.cs
├── Repositories/
│   ├── ProductoRepository.cs
│   └── PresupuestoRepository.cs
├── Views/
│   ├── Productos/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   └── Delete.cshtml
│   ├── Presupuestos/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   ├── Delete.cshtml
│   │   └── Details.cshtml
│   └── Shared/
│       └── _Layout.cshtml
├── DB/
│   ├── tienda.db
│   └── init.sql
└── InitDatabase.cs
```

## Requisitos
- .NET 8.0 SDK o superior
- Navegador web moderno

## Instalación y Ejecución

1. Navegar al directorio del proyecto:
```bash
cd TP8_MVC
```

2. Restaurar dependencias:
```bash
dotnet restore
```

3. Ejecutar la aplicación:
```bash
dotnet run
```

4. Abrir el navegador en:
```
https://localhost:5001
```
o
```
http://localhost:5000
```

## Base de Datos
La base de datos SQLite se crea automáticamente al iniciar la aplicación por primera vez.
Se incluyen datos de ejemplo para facilitar las pruebas:
- 5 productos
- 3 presupuestos con productos asociados

## Uso
1. **Productos**: 
   - Click en "Productos" en el menú de navegación
   - Crear nuevos productos, editarlos o eliminarlos

2. **Presupuestos**:
   - Click en "Presupuestos" en el menú de navegación
   - Crear nuevos presupuestos
   - Ver detalles para visualizar productos asociados y monto total
   - Editar o eliminar presupuestos existentes

## Tecnologías Utilizadas
- ASP.NET Core 8.0 MVC
- Microsoft.Data.SQLite
- Bootstrap 5
- Razor Views

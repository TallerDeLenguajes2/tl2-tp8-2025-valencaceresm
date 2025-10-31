# Resumen - TP8 Completado

## ✅ Trabajo Práctico N° 8 - ASP.NET Core MVC

### Proyecto Creado: TP8_MVC

## Estructura Implementada

### 📁 Modelos (Models/)
- ✅ **Producto.cs**: Modelo con IdProducto, Descripcion, Cantidad, Precio
- ✅ **Presupuesto.cs**: Modelo con IdPresupuesto, NombreDestinatario, Lista de Productos y método MontoTotal()

### 📁 Repositorios (Repositories/)
- ✅ **ProductoRepository.cs**: Implementa patrón Repository con SQLite
  - GetAll(), GetById(), Create(), Update(), Delete()
- ✅ **PresupuestoRepository.cs**: Implementa patrón Repository con SQLite
  - GetAll(), GetById(), Create(), Update(), Delete()
  - AddProducto(), RemoveProducto()

### 📁 Controladores (Controllers/)
- ✅ **ProductosController.cs**: CRUD completo
  - Index (GET) - Listar productos
  - Create (GET/POST) - Crear producto
  - Edit (GET/POST) - Editar producto
  - Delete (GET/POST) - Eliminar producto

- ✅ **PresupuestosController.cs**: CRUD completo
  - Index (GET) - Listar presupuestos
  - Details (GET) - Ver detalle con productos y monto total
  - Create (GET/POST) - Crear presupuesto
  - Edit (GET/POST) - Editar presupuesto
  - Delete (GET/POST) - Eliminar presupuesto

### 📁 Vistas (Views/)

#### Productos/
- ✅ **Index.cshtml**: Lista de productos con tabla Bootstrap
- ✅ **Create.cshtml**: Formulario para crear producto
- ✅ **Edit.cshtml**: Formulario para editar producto
- ✅ **Delete.cshtml**: Confirmación de eliminación

#### Presupuestos/
- ✅ **Index.cshtml**: Lista de presupuestos con tabla Bootstrap
- ✅ **Details.cshtml**: Detalle del presupuesto con:
  - Encabezado del presupuesto
  - Listado de productos asociados
  - Monto total calculado
- ✅ **Create.cshtml**: Formulario para crear presupuesto
- ✅ **Edit.cshtml**: Formulario para editar presupuesto
- ✅ **Delete.cshtml**: Confirmación de eliminación

#### Shared/
- ✅ **_Layout.cshtml**: Modificado con menú de navegación
  - Enlace a Productos
  - Enlace a Presupuestos

### 📁 Base de Datos (DB/)
- ✅ **tienda.db**: Base de datos SQLite (se crea automáticamente)
- ✅ **init.sql**: Script SQL de inicialización
- ✅ **InitDatabase.cs**: Clase para inicializar DB al arrancar

#### Tablas Creadas:
1. **Productos** (idProducto, Descripcion, Cantidad, Precio)
2. **Presupuestos** (idPresupuesto, NombreDestinatario, FechaCreacion)
3. **PresupuestosDetalle** (idPresupuestoDetalle, idPresupuesto, idProducto, Cantidad)

#### Datos de Ejemplo:
- 5 productos
- 3 presupuestos con productos asociados

## Características Implementadas

✅ Patrón MVC correctamente aplicado
✅ Bootstrap para diseño responsivo
✅ Operaciones CRUD completas
✅ Validación de formularios
✅ Integración con SQLite
✅ Inicialización automática de base de datos
✅ Datos de ejemplo incluidos

## Cómo Ejecutar

1. Navegar al directorio del proyecto:
```bash
cd TP8_MVC
```

2. Ejecutar la aplicación:
```bash
dotnet run
```

3. Abrir en navegador:
- https://localhost:5001
- http://localhost:5000

## Verificación de Requisitos

### Etapa 1: Patrón MVC Básico y Lectura de Datos ✅
- [x] Estructura MVC creada con Bootstrap
- [x] Modelo Producto con propiedades requeridas
- [x] Modelo Presupuesto con propiedades requeridas
- [x] ProductosController con Index
- [x] PresupuestosController con Index y Details
- [x] _Layout.cshtml modificado con menú "Productos" y "Presupuestos"

### Etapa 2: Persistencia y Control de Entidades ✅
- [x] ProductoRepository con SQLite
  - [x] Create (GET y POST)
  - [x] Edit (GET y POST)
  - [x] Delete (GET y POST)
  
- [x] PresupuestoRepository con SQLite
  - [x] Create (GET y POST)
  - [x] Edit (GET y POST)
  - [x] Delete (GET y POST)
  
- [x] Vista Details en PresupuestosController
  - [x] Encabezado del Presupuesto
  - [x] Listado de todos los Productos asociados
  - [x] Monto total calculado

## Estado: ✅ COMPLETADO

Todos los requisitos del TP N° 8 han sido implementados exitosamente.
El proyecto compila sin errores y está listo para ser ejecutado y probado.

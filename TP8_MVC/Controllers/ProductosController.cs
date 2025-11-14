using Microsoft.AspNetCore.Mvc;
using TP8_MVC.Interfaces;
using TP8_MVC.Models;
using TP8_MVC.ViewModels;

namespace TP8_MVC.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IAuthenticationService _authenticationService;

        public ProductosController(IProductoRepository productoRepository, IAuthenticationService authenticationService)
        {
            _productoRepository = productoRepository;
            _authenticationService = authenticationService;
        }

        // GET: Productos
        public IActionResult Index()
        {
            // Solo administradores pueden gestionar productos
            if (!_authenticationService.IsAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

            if (!_authenticationService.HasAccessLevel("Administrador"))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            var productos = _productoRepository.GetAll();
            return View(productos);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            if (!_authenticationService.IsAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

            if (!_authenticationService.HasAccessLevel("Administrador"))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            return View(new ProductoViewModel());
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductoViewModel productoVM)
        {
            if (!_authenticationService.IsAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

            if (!_authenticationService.HasAccessLevel("Administrador"))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            // 1. Chequeo de seguridad del servidor
            if (!ModelState.IsValid)
            {
                // Si falla: devolvemos el ViewModel con los datos y errores a la Vista
                return View(productoVM);
            }

            // 2. Si es válido: Mapeo manual de VM a Modelo de Dominio
            var nuevoProducto = new Producto
            {
                Descripcion = productoVM.Descripcion ?? string.Empty,
                Precio = productoVM.Precio,
                Cantidad = 0 // La cantidad se maneja al agregar productos a presupuestos
            };

            // 3. Llamada al repositorio
            _productoRepository.Create(nuevoProducto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Productos/Edit/5
        public IActionResult Edit(int id)
        {
            if (!_authenticationService.IsAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

            if (!_authenticationService.HasAccessLevel("Administrador"))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            var producto = _productoRepository.GetById(id);
            if (producto == null)
            {
                return NotFound();
            }

            // Mapeo de Modelo de Dominio a ViewModel
            var productoVM = new ProductoViewModel
            {
                IdProducto = producto.IdProducto,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio
            };

            return View(productoVM);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductoViewModel productoVM)
        {
            if (!_authenticationService.IsAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

            if (!_authenticationService.HasAccessLevel("Administrador"))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            if (id != productoVM.IdProducto)
            {
                return NotFound();
            }

            // 1. Chequeo de seguridad del servidor
            if (!ModelState.IsValid)
            {
                return View(productoVM);
            }

            // 2. Mapeo manual VM -> Modelo de Dominio
            var productoEditado = new Producto
            {
                IdProducto = productoVM.IdProducto,
                Descripcion = productoVM.Descripcion ?? string.Empty,
                Precio = productoVM.Precio,
                Cantidad = 0
            };

            // 3. Llamada al repositorio
            _productoRepository.Update(productoEditado);
            return RedirectToAction(nameof(Index));
        }

        // GET: Productos/Delete/5
        public IActionResult Delete(int id)
        {
            if (!_authenticationService.IsAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

            if (!_authenticationService.HasAccessLevel("Administrador"))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            var producto = _productoRepository.GetById(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!_authenticationService.IsAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

            if (!_authenticationService.HasAccessLevel("Administrador"))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            _productoRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

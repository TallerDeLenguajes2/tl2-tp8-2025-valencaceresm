using Microsoft.AspNetCore.Mvc;
using TP8_MVC.Models;
using TP8_MVC.Repositories;

namespace TP8_MVC.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ProductoRepository _productoRepository;

        public ProductosController()
        {
            _productoRepository = new ProductoRepository();
        }

        // GET: Productos
        public IActionResult Index()
        {
            var productos = _productoRepository.GetAll();
            return View(productos);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _productoRepository.Create(producto);
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productos/Edit/5
        public IActionResult Edit(int id)
        {
            var producto = _productoRepository.GetById(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _productoRepository.Update(producto);
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productos/Delete/5
        public IActionResult Delete(int id)
        {
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
            _productoRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

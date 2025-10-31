using Microsoft.AspNetCore.Mvc;
using TP8_MVC.Models;
using TP8_MVC.Repositories;

namespace TP8_MVC.Controllers
{
    public class PresupuestosController : Controller
    {
        private readonly PresupuestoRepository _presupuestoRepository;
        private readonly ProductoRepository _productoRepository;

        public PresupuestosController()
        {
            _presupuestoRepository = new PresupuestoRepository();
            _productoRepository = new ProductoRepository();
        }

        // GET: Presupuestos
        public IActionResult Index()
        {
            var presupuestos = _presupuestoRepository.GetAll();
            return View(presupuestos);
        }

        // GET: Presupuestos/Details/5
        public IActionResult Details(int id)
        {
            var presupuesto = _presupuestoRepository.GetById(id);
            if (presupuesto == null)
            {
                return NotFound();
            }
            return View(presupuesto);
        }

        // GET: Presupuestos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Presupuestos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Presupuesto presupuesto)
        {
            if (ModelState.IsValid)
            {
                _presupuestoRepository.Create(presupuesto);
                return RedirectToAction(nameof(Index));
            }
            return View(presupuesto);
        }

        // GET: Presupuestos/Edit/5
        public IActionResult Edit(int id)
        {
            var presupuesto = _presupuestoRepository.GetById(id);
            if (presupuesto == null)
            {
                return NotFound();
            }
            return View(presupuesto);
        }

        // POST: Presupuestos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Presupuesto presupuesto)
        {
            if (id != presupuesto.IdPresupuesto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _presupuestoRepository.Update(presupuesto);
                return RedirectToAction(nameof(Index));
            }
            return View(presupuesto);
        }

        // GET: Presupuestos/Delete/5
        public IActionResult Delete(int id)
        {
            var presupuesto = _presupuestoRepository.GetById(id);
            if (presupuesto == null)
            {
                return NotFound();
            }
            return View(presupuesto);
        }

        // POST: Presupuestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _presupuestoRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

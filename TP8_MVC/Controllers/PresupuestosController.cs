using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TP8_MVC.Interfaces;
using TP8_MVC.Models;
using TP8_MVC.ViewModels;

namespace TP8_MVC.Controllers
{
    public class PresupuestosController : Controller
    {
        private readonly IPresupuestoRepository _presupuestoRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IAuthenticationService _authenticationService;

        public PresupuestosController(IPresupuestoRepository presupuestoRepository, IProductoRepository productoRepository, IAuthenticationService authenticationService)
        {
            _presupuestoRepository = presupuestoRepository;
            _productoRepository = productoRepository;
            _authenticationService = authenticationService;
        }

        // GET: Presupuestos
        public IActionResult Index()
        {
            if (!_authenticationService.IsAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

            // Administrador y Cliente pueden ver el listado
            var presupuestos = _presupuestoRepository.GetAll();
            return View(presupuestos);
        }

        // GET: Presupuestos/Details/5
        public IActionResult Details(int id)
        {
            if (!_authenticationService.IsAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

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
            if (!_authenticationService.IsAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

            if (!_authenticationService.HasAccessLevel("Administrador"))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            return View(new PresupuestoViewModel());
        }

        // POST: Presupuestos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PresupuestoViewModel presupuestoVM)
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
                return View(presupuestoVM);
            }

            // 2. Mapeo manual VM -> Modelo de Dominio
            var presupuesto = new Presupuesto
            {
                NombreDestinatario = presupuestoVM.NombreDestinatario
            };

            // 3. Guardar en base de datos
            _presupuestoRepository.Create(presupuesto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Presupuestos/Edit/5
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

            var presupuesto = _presupuestoRepository.GetById(id);
            if (presupuesto == null)
            {
                return NotFound();
            }

            var presupuestoVM = new PresupuestoViewModel
            {
                IdPresupuesto = presupuesto.IdPresupuesto,
                NombreDestinatario = presupuesto.NombreDestinatario
            };

            return View(presupuestoVM);
        }

        // POST: Presupuestos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PresupuestoViewModel presupuestoVM)
        {
            if (!_authenticationService.IsAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

            if (!_authenticationService.HasAccessLevel("Administrador"))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            if (id != presupuestoVM.IdPresupuesto)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(presupuestoVM);
            }

            var presupuesto = new Presupuesto
            {
                IdPresupuesto = presupuestoVM.IdPresupuesto,
                NombreDestinatario = presupuestoVM.NombreDestinatario
            };

            _presupuestoRepository.Update(presupuesto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Presupuestos/Delete/5
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
            if (!_authenticationService.IsAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

            if (!_authenticationService.HasAccessLevel("Administrador"))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            _presupuestoRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Presupuestos/AgregarProducto/5
        public IActionResult AgregarProducto(int id)
        {
            if (!_authenticationService.IsAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

            if (!_authenticationService.HasAccessLevel("Administrador"))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            var presupuesto = _presupuestoRepository.GetById(id);
            if (presupuesto == null)
            {
                return NotFound();
            }

            var productos = _productoRepository.GetAll();
            var vm = new AgregarProductoViewModel
            {
                IdPresupuesto = presupuesto.IdPresupuesto,
                ListaProductos = new SelectList(productos, "IdProducto", "Descripcion")
            };

            return View(vm);
        }

        // POST: Presupuestos/AgregarProducto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarProducto(AgregarProductoViewModel model)
        {
            if (!_authenticationService.IsAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

            if (!_authenticationService.HasAccessLevel("Administrador"))
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }

            // 1. Chequeo de seguridad por la Cantidad y selección
            if (!ModelState.IsValid)
            {
                // Lógica crítica de recarga: recargar el SelectList si falla la validación
                var productos = _productoRepository.GetAll();
                model.ListaProductos = new SelectList(productos, "IdProducto", "Descripcion");
                return View(model);
            }

            // 2. Si es válido: llamamos al repositorio para guardar la relación
            _presupuestoRepository.AddProducto(model.IdPresupuesto, model.IdProducto!.Value, model.Cantidad);

            // 3. Redirigimos al detalle del presupuesto
            return RedirectToAction(nameof(Details), new { id = model.IdPresupuesto });
        }
    }
}

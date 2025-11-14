using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TP8_MVC.ViewModels
{
    // Maneja el formulario para agregar un Producto a un Presupuesto
    public class AgregarProductoViewModel
    {
        // ID del presupuesto al que se va a agregar el producto
        public int IdPresupuesto { get; set; }

        // ID del producto seleccionado en el dropdown
        [Display(Name = "Producto a agregar")]
        [Required(ErrorMessage = "Debe seleccionar un producto.")]
        public int? IdProducto { get; set; }

        // Cantidad del producto
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero.")]
        public int Cantidad { get; set; }

        // Lista de productos disponible para el dropdown
        public SelectList? ListaProductos { get; set; }
    }
}

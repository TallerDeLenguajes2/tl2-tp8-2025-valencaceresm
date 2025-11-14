using System.ComponentModel.DataAnnotations;

namespace TP8_MVC.ViewModels
{
    // Maneja la creación y edición de Productos
    public class ProductoViewModel
    {
        // Se incluye para la acción de EDICIÓN
        public int IdProducto { get; set; }

        [Display(Name = "Descripción del Producto")]
        [StringLength(250, ErrorMessage = "La descripción no puede superar los 250 caracteres.")]
        public string? Descripcion { get; set; }

        [Display(Name = "Precio Unitario")]
        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
        public decimal Precio { get; set; }
    }
}

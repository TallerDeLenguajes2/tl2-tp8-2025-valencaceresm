using System.ComponentModel.DataAnnotations;

namespace TP8_MVC.ViewModels
{
    // Maneja la creación y edición de Presupuestos
    public class PresupuestoViewModel
    {
        public int IdPresupuesto { get; set; }

        [Display(Name = "Nombre o Email del Destinatario")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string NombreDestinatario { get; set; } = string.Empty;

        // Opcional: se podría guardar un email
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        public string? EmailDestinatario { get; set; }
    }
}

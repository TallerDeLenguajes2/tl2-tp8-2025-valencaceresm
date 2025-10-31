namespace TP8_MVC.Models
{
    public class Presupuesto
    {
        public int IdPresupuesto { get; set; }
        public string NombreDestinatario { get; set; } = string.Empty;
        public List<Producto> Productos { get; set; } = new List<Producto>();
        
        public decimal MontoTotal()
        {
            return Productos.Sum(p => p.Precio * p.Cantidad);
        }
    }
}

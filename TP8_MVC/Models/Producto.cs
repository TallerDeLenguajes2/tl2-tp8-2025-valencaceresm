namespace TP8_MVC.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}

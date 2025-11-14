using TP8_MVC.Models;

namespace TP8_MVC.Interfaces;

public interface IProductoRepository
{
    List<Producto> GetAll();
    Producto? GetById(int id);
    void Create(Producto producto);
    void Update(Producto producto);
    void Delete(int id);
}

using TP8_MVC.Models;

namespace TP8_MVC.Interfaces;

public interface IPresupuestoRepository
{
    List<Presupuesto> GetAll();
    Presupuesto? GetById(int id);
    void Create(Presupuesto presupuesto);
    void Update(Presupuesto presupuesto);
    void Delete(int id);
    void AddProducto(int idPresupuesto, int idProducto, int cantidad);
    void RemoveProducto(int idPresupuesto, int idProducto);
}

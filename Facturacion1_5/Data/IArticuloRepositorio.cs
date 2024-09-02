using Facturacion1_5.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1_5.Data
{
    public interface IArticuloRepositorio
    {
        List<Articulo> DevolverTodos();
        Articulo ObtenerPorId(int id);
        bool Guardar(Articulo oArticulo);
        bool Remover(int id);
    }
}

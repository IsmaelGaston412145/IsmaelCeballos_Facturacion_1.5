using Facturacion1_5.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1_5.Data
{
    public interface IFacturaRepositorio
    {
        bool Guardar(Factura oFactura);

        List<Factura> DevolverTodos();

        Factura ObtenerPorId(int id);
    }
}

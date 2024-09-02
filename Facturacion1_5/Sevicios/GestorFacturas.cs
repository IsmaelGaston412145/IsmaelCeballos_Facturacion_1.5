using Facturacion1_5.Data;
using Facturacion1_5.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1_5.Sevicios
{
    public class GestorFacturas
    {
        private FacturaRepositorio facturaRepos;

        public GestorFacturas()
        {
            facturaRepos = new FacturaRepositorio();
        }

        public List<Factura> DevolverFacturas()
        {
            return facturaRepos.DevolverTodos();
        }

        public Factura FacturaPorId(int id)
        {
            return facturaRepos.ObtenerPorId(id);
        }

        public bool GuardarFactura(Factura oFactura)
        {
            return facturaRepos.Guardar(oFactura);
        }

    }
}

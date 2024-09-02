using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1_5.Dominio
{
    public class DetallesFactura
    {
        public int Cantidad { get; set; }

        public Articulo Articulo { get; set; }

        public override string ToString()
        {
            return $"[{Articulo.ToString()}/ Cantidad: {Cantidad}]";
        }

    }
}

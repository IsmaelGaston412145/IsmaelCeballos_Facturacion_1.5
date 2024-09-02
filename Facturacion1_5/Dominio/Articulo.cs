using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1_5.Dominio
{
    public class Articulo
    {
        public string Nombre { get; set; }

        public SqlMoney PrecioUnit { get; set; }

        public int Codigo { get; set; }

        public int Activo { get; set; }

        public override string ToString()
        {
            if (Activo == 1)
            {
                return $"(Art: {Nombre}/ Pre_unitario: {PrecioUnit}/ Activo)";
            }
            else
            {
                return $"(Art: {Nombre}/ Pre_unitario: {PrecioUnit}/ Inactivo)";
            }
        }

    }
}

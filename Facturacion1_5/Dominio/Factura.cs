using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1_5.Dominio
{
    public class Factura
    {
        public int NroFactura { get; set; }

        public DateTime Fecha { get; set; }

        public FormaPago FormaPago { get; set; }

        public string Cliente { get; set; }

        private List<DetallesFactura> detalles;

        public Factura()
        {
            detalles = new List<DetallesFactura>();
        }

        public List<DetallesFactura> TraerDetalles()
        {
            return detalles;
        }

        public void AgregarDetalle(DetallesFactura detalle)
        {
            bool contiene = false;
            if (detalle != null)
            {
                foreach (DetallesFactura d in detalles)
                {
                    if (d.Articulo == detalle.Articulo)
                    {
                        d.Cantidad = d.Cantidad + detalle.Cantidad;
                        contiene = true;
                    }
                }
                if (!contiene)
                {
                    detalles.Add(detalle);
                }
            }
        }

        public void QuitarDetalle(int id)
        {
            detalles.RemoveAt(id);
        }

        public override string ToString()
        {
            return $"Cod: {NroFactura}| Fec: {Fecha}| F_Pago: {FormaPago.ToString()}| Cli: {Cliente}| Det_Cant: {detalles.Count}";
        }

    }
}
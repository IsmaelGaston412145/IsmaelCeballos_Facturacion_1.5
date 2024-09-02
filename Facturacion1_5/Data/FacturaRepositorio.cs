using Facturacion1_5.Data.Utilidades;
using Facturacion1_5.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1_5.Data
{
    public class FacturaRepositorio : IFacturaRepositorio
    {
        public List<Factura> DevolverTodos()
        {
            List<Factura> lst = new List<Factura>();
            var helper = DataHelper.GetInstance();
            var t = helper.EjecutarConsultaPA("PA_RECUPERAR_FACTURAS", null);
            foreach (DataRow row in t.Rows)
            {
                //Añadir factura
                int nroFactura = Convert.ToInt32(row["nro_factura"]);
                DateTime fecha = Convert.ToDateTime(row["fecha"]);
                FormaPago formaPago = new FormaPago();
                formaPago.Codigo = Convert.ToInt32(row["id_forma_pago"]);
                formaPago.Nombre = row["forma_pago"].ToString();
                string cliente = row["cliente"].ToString();

                Factura oFactura = new Factura()
                {
                    NroFactura = nroFactura,
                    Fecha = fecha,
                    FormaPago = formaPago,
                    Cliente = cliente
                };

                //Añadir detalles
                var parametro = new List<ParametroSQL>();
                parametro.Add(new ParametroSQL("@id", nroFactura));
                var td = helper.EjecutarConsultaPA("PA_RECUPERAR_DETALLES", parametro);
                foreach (DataRow dRow in td.Rows)
                {
                    int cantidad = Convert.ToInt32(dRow["cantidad"]);
                    var articulo = new Articulo();
                    articulo.Codigo = Convert.ToInt32(dRow["id_articulo"]);
                    articulo.Nombre = dRow["nombre"].ToString();
                    articulo.PrecioUnit = Convert.ToDecimal(dRow["pre_unitario"]);
                    articulo.Activo = Convert.ToInt32(dRow["activo"]);

                    var detalle = new DetallesFactura()
                    {
                        Cantidad = cantidad,
                        Articulo = articulo
                    };

                    oFactura.AgregarDetalle(detalle);
                }
                
                lst.Add(oFactura);
            }
            return lst;
        }

        public Factura ObtenerPorId(int id)
        {
            var parameters = new List<ParametroSQL>();
            parameters.Add(new ParametroSQL("@codigo", id));
            DataTable t = DataHelper.GetInstance().EjecutarConsultaPA("PA_RECUPERAR_FACTURA_POR_CODIGO", parameters);

            if (t != null && t.Rows.Count == 1)
            {
                DataRow row = t.Rows[0];
                int nroFactura = Convert.ToInt32(row["nro_factura"]);
                DateTime fecha = Convert.ToDateTime(row["fecha"]);
                FormaPago formaPago = new FormaPago();
                formaPago.Codigo = Convert.ToInt32(row["id_forma_pago"]);
                formaPago.Nombre = row["forma_pago"].ToString();
                string cliente = row["cliente"].ToString();

                Factura oFactura = new Factura()
                {
                    NroFactura = nroFactura,
                    Fecha = fecha,
                    FormaPago = formaPago,
                    Cliente = cliente
                };
                return oFactura;

            }
            return null;
        }

        public bool Guardar(Factura oFactura)
        {
            bool result = true;
            string query = "PA_GUARDAR_FACTURA";
            DataHelper helper = DataHelper.GetInstance();
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@forma_pago", oFactura.FormaPago.Codigo));
            parametros.Add(new SqlParameter("@cliente", oFactura.Cliente));

            if (helper.EjecutarDMLTransaccion(query, parametros, oFactura) == 0)
            {
                result = false;
            }

            return result;
        }
    }
}

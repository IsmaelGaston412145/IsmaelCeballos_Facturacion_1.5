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
    public class ArticuloRepositorio : IArticuloRepositorio
    {
        public bool Remover(int id)
        {
            var parameters = new List<ParametroSQL>();
            parameters.Add(new ParametroSQL("@codigo", id));
            int rows = DataHelper.GetInstance().EjecutarDMLPA("PA_REGISTRAR_BAJA_ARTICULO", parameters);
            return rows == 1;
        }

        public List<Articulo> DevolverTodos()
        {
            List<Articulo> lst = new List<Articulo>();
            var helper = DataHelper.GetInstance();
            var t = helper.EjecutarConsultaPA("PA_RECUPERAR_ARTICULOS", null);
            foreach (DataRow row in t.Rows)
            {
                int codigo = Convert.ToInt32(row["id_articulo"]);
                string nombre = row["nombre"].ToString();
                SqlMoney pre_unitario = Convert.ToDecimal(row["pre_unitario"]);
                int activo = Convert.ToInt32(row["activo"]);


                Articulo oArticulo = new Articulo()
                {
                    Codigo = codigo,
                    Nombre = nombre,
                    PrecioUnit = pre_unitario,
                    Activo = activo
                };
                lst.Add(oArticulo);
            }
            return lst;
        }

        public Articulo ObtenerPorId(int id)
        {
            var parameters = new List<ParametroSQL>();
            parameters.Add(new ParametroSQL("@codigo", id));
            DataTable t = DataHelper.GetInstance().EjecutarConsultaPA("PA_RECUPERAR_ARTICULO_POR_CODIGO", parameters);

            if (t != null && t.Rows.Count == 1)
            {
                DataRow row = t.Rows[0];
                int codigo = Convert.ToInt32(row["id_articulo"]);
                string nombre = row["nombre"].ToString();
                SqlMoney pre_unitario = Convert.ToDecimal(row["pre_unitario"]);
                int activo = Convert.ToInt32(row["activo"]);


                Articulo oArticulo = new Articulo()
                {
                    Codigo = codigo,
                    Nombre = nombre,
                    PrecioUnit = pre_unitario,
                    Activo = activo
                };
                return oArticulo;

            }
            return null;
        }

        public bool Guardar(Articulo oArticulo)
        {
            bool result = true;
            string query = "PA_GUARDAR_ARTICULO";
            DataHelper helper = DataHelper.GetInstance();
            if (oArticulo != null)
            {
                var parameters = new List<ParametroSQL>();
                parameters.Add(new ParametroSQL("@codigo", oArticulo.Codigo));
                parameters.Add(new ParametroSQL("@nombre", oArticulo.Nombre));
                parameters.Add(new ParametroSQL("@pre_unitario", oArticulo.PrecioUnit));
                int rows = DataHelper.GetInstance().EjecutarDMLPA(query, parameters);
                return rows == 1;
            }
            return result;
        }
    }
}

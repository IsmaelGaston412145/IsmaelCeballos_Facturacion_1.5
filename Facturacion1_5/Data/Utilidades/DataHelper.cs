using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturacion1_5.Dominio;

namespace Facturacion1_5.Data.Utilidades
{
    public class DataHelper
    {
        private static DataHelper _instancia;
        private SqlConnection _conexion;

        private DataHelper()
        {
            _conexion = new SqlConnection(Properties.Resources.cnnString);
        }

        public static DataHelper GetInstance()
        {
            if (_instancia == null)
                _instancia = new DataHelper();

            return _instancia;
        }

        public DataTable EjecutarConsultaPA(string sp, List<ParametroSQL>? parametros)
        {
            DataTable t = new DataTable();
            try
            {
                _conexion.Open();
                var cmd = new SqlCommand(sp, _conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var param in parametros)
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                }

                t.Load(cmd.ExecuteReader());
                _conexion.Close();
            }
            catch (SqlException)
            {
                t = null;
            }

            return t;
        }

        public int EjecutarDMLPA(string sp, List<ParametroSQL>? parametros)
        {
            int rows;
            try
            {
                _conexion.Open();
                var cmd = new SqlCommand(sp, _conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var param in parametros)
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                }

                rows = cmd.ExecuteNonQuery();
                _conexion.Close();
            }
            catch (SqlException)
            {
                rows = 0;
            }

            return rows;
        }

        public int EjecutarDMLTransaccion(string sp, List<SqlParameter> parametros, Factura factura)
        {
            int registros = 0;
            SqlTransaction? t = null;
            try
            {
                _conexion.Open();
                t = _conexion.BeginTransaction();

                var cmd = new SqlCommand(sp, _conexion, t);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Parámetro de entrada
                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        cmd.Parameters.Add(parametro);
                    }
                }
                //parámetro de salida
                SqlParameter param = new SqlParameter("@id", System.Data.SqlDbType.Int);
                param.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(param);

                registros = cmd.ExecuteNonQuery();

                int facturaId = (int)param.Value;

                int nro_detail = 1;
                foreach (var detalle in factura.TraerDetalles())
                {
                    var cmdDetalle = new SqlCommand("PA_INSERTAR_DETALLE", _conexion, t);
                    cmdDetalle.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdDetalle.Parameters.AddWithValue("@nro_factura", facturaId);
                    cmdDetalle.Parameters.AddWithValue("@id_detalle", nro_detail);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@id_producto", detalle.Articulo.Codigo);

                    cmdDetalle.ExecuteNonQuery();
                    nro_detail++;
                }

                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null)
                {
                    t.Rollback();
                    Console.WriteLine("\nUn error ah ocurrido");
                }

                registros = 0;
            }
            finally
            {
                if (_conexion != null && _conexion.State == System.Data.ConnectionState.Open)
                {
                    _conexion.Close();
                }
            }
            return registros;
        }

        public SqlConnection DevolverConexion()
        {
            //devolver una connection
            return _conexion;
        }

    }
}

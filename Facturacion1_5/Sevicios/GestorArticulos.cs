using Facturacion1_5.Data;
using Facturacion1_5.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1_5.Sevicios
{
    public class GestorArticulos
    {
        private ArticuloRepositorio articuloRepos;

        public GestorArticulos()
        {
            articuloRepos = new ArticuloRepositorio();
        }

        public bool RemoverArticulo(int id)
        {
            return articuloRepos.Remover(id);
        }

        public List<Articulo> DevolverArticulos()
        {
            return articuloRepos.DevolverTodos();
        }

        public Articulo ArticuloPorId(int id)
        {
            return articuloRepos.ObtenerPorId(id);
        }

        public bool GuardarArticulo(Articulo oArticulo)
        {
            return articuloRepos.Guardar(oArticulo);
        }

    }
}

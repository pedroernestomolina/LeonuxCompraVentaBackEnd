using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Producto.Lista
{
    
    public class Filtro
    {

        public string cadena { get; set; }
        public string autoDepartamento { get; set; }
        public string autoGrupo { get; set; }
        public string autoProveedor { get; set; }
        public string autoMarca { get; set; }
        public Enumerados.EnumMetodoBusqueda MetodoBusqueda { get; set; }


        public Filtro()
        {
            cadena = "";
            autoDepartamento = "";
            autoGrupo = "";
            autoProveedor = "";
            autoMarca = "";
            MetodoBusqueda = Enumerados.EnumMetodoBusqueda.SnDefinir;
        }

    }

}
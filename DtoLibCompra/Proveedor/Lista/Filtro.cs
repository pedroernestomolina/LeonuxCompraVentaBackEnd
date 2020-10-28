using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Proveedor.Lista
{
    
    public class Filtro
    {

        public string cadena { get; set; }
        public string autoGrupo { get; set; }
        public string autoEstado { get; set; }
        public Enumerados.EnumEstatus estatus { get; set; }
        public Enumerados.EnumMetodoBusqueda MetodoBusqueda { get; set; }


        public Filtro()
        {
            cadena = "";
            autoEstado = "";
            autoGrupo = "";
            estatus = Enumerados.EnumEstatus.SnDefinir;
            MetodoBusqueda = Enumerados.EnumMetodoBusqueda.SnDefinir;
        }

    }

}
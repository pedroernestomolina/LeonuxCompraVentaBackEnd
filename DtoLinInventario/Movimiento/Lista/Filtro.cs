using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Lista
{
    
    public class Filtro
    {

        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public  enumerados.EnumTipoDocumento TipoDocumento { get; set; }
        public string IdSucursal { get; set; }


        public Filtro()
        {
            Desde = null;
            Hasta = null;
            TipoDocumento = enumerados.EnumTipoDocumento.SinDefinir;
            IdSucursal = "";
        }

    }

}
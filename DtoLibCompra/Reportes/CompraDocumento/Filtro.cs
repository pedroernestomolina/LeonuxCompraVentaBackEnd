using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Reportes.CompraDocumento
{
    
    public class Filtro
    {

        public string codSucursal { get; set; }
        public Enumerados.EnumEstatus estatus { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }


        public Filtro()
        {
            codSucursal = "";
            estatus = Enumerados.EnumEstatus.SinDefinir;
            desde = DateTime.Now.Date;
            hasta = DateTime.Now.Date;
        }

    }

}
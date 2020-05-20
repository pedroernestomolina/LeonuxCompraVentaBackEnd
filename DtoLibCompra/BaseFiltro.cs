using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra
{

    public class BaseFiltro
    {
        public string cadena { get; set; }
        public DateTime? segun_FechaEmisionDesde { get; set; }
        public DateTime? segun_FechaEmisionHasta { get; set; }


        public BaseFiltro() 
        {
            cadena = "";
            segun_FechaEmisionDesde = null;
            segun_FechaEmisionHasta = null;
        }
    }

}

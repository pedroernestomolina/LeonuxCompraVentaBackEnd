using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Proveedor.Articulos
{
    
    public class Filtro
    {

        public string autoProv { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }


        public Filtro()
        {
            autoProv = "";
            desde = DateTime.Now.Date;
            hasta = DateTime.Now.Date;
        }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Producto.CodigoRefProveedor
{
    
    public class Filtro
    {

        public string autoPrd { get; set; }
        public string autoPrv { get; set; }


        public Filtro()
        {
            autoPrd = "";
            autoPrv = "";
        }

    }

}
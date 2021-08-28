using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.ResumenVentaPos.Entidad
{
    
    public class Ficha
    {

        public string codSucursal { get; set; }
        public string autoProducto { get; set; }
        public decimal cnt { get; set; }


        public Ficha() 
        {
            codSucursal = "";
            autoProducto = "";
            cnt = 0.0m;
        }

    }

}
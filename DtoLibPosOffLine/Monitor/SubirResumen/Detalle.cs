using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Monitor.SubirResumen
{
    
    public class Detalle
    {

        public string autoProducto { get; set; }
        public decimal cnt { get; set; }


        public Detalle() 
        {
            autoProducto = "";
            cnt = 0.0m;
        }

    }

}
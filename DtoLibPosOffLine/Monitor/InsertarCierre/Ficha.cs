using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Monitor.InsertarCierre
{
    
    public class Ficha
    {

        public string cierre { get; set; }
        public string estatus { get; set; }


        public Ficha() 
        {
            cierre = "";
            estatus = "";
        }

    }

}
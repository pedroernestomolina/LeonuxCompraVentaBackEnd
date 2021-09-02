using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Monitor.SubirResumen
{
    
    public class Ficha
    {

        public string codSucursal { get; set; }
        public string cierre { get; set; }
        public List<Detalle> Lista { get; set; }


        public Ficha()
        {
            codSucursal = "";
            cierre = "";
            Lista = new List<Detalle>();
        }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.ResumenVentaPos.Subir
{
    
    public class Ficha
    {

        public string codSucursal { get; set; }
        public List<Detalle> detalles { get; set; }

        public Ficha() 
        {
            codSucursal = "";
            detalles = new List<Detalle>();
        }

    }

}
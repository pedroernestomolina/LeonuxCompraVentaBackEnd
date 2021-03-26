using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Agregar.Factura
{
    
    public class FichaPrdCosto
    {

        public string autoPrd { get; set; }
        public decimal costo { get; set; }
        public decimal costoUnd { get; set; }
        public decimal costoDivisa { get; set; }
        public decimal cntUnd { get; set; }
        public int contenido { get; set; }
        public string nombrePrd { get; set; }


        public FichaPrdCosto()
        {
            autoPrd = "";
            costo = 0.0m;
            costoUnd = 0.0m;
            costoDivisa = 0.0m;
            cntUnd = 0.0m;
            contenido = 0;
            nombrePrd = "";
        }

    }

}
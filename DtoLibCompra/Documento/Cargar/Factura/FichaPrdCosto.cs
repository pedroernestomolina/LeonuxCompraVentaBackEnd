using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Cargar.Factura
{
    
    public class FichaPrdCosto
    {

        public string autoPrd { get; set; }
        public decimal costo { get; set; }
        public decimal costoUnd { get; set; }
        public decimal costoDivisa { get; set; }
        public decimal cntUnd { get; set; }
        public int contenido { get; set; }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Agregar.Factura
{
    
    public class FichaPrdCostoHistorico
    {

        public string autoPrd { get; set; }
        public string nota { get; set; }
        public decimal costo { get; set; }
        public decimal costoDivisa { get; set; }
        public decimal tasaDivisa { get; set; }
        public string serie { get; set; }
        public string documento { get; set; }

    }

}
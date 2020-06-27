using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Configuracion.Serie
{
    
    public class Ficha
    {

        public string ParaFactura { get; set; }
        public string ParaNotaCredito { get; set; }
        public string ParaNotaDebito { get; set; }
        public string ParaNotaEntrega { get; set; }
        public string ControlParaFactura { get; set; }
        public string ControlParaNotaCredito { get; set; }
        public string ControlParaNotaDebito { get; set; }
        public string ControlParaNotaEntrega { get; set; }
        public int CorrelativoParaFactura { get; set; }
        public int CorrelativoParaNtCredito { get; set; }
        public int CorrelativoParaNtDebito { get; set; }
        public int CorrelativoParaNtEntrega { get; set; }

    }

}
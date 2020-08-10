using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Reporte.Pago.Resumen
{
    
    public class Ficha
    {

        public string codigo { get; set; }
        public string descripcion { get; set; }
        public decimal importe { get; set; }
        public decimal montoRecibido { get; set; }
        public decimal tasa { get; set; }
        public string lote { get; set; }
        public string referencia { get; set; }
        public Enumerados.enumTipoMedioCobro tipoMedioCobro { get; set; }

    }

}
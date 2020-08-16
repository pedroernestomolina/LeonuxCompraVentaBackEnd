using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Reporte.Pago.Resumen
{
    
    public class Ficha
    {

        public List<Detalle> detalle { get; set; }
        public decimal montoNCredito { get; set; }
        public decimal montoCambioDar { get; set; }

    }

}
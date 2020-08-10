using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Reporte.Pago.Detalle
{
    
    public class Pago
    {

        public string codigo { get; set; }
        public string descripcion { get; set; }
        public decimal importe { get; set; }
        public decimal montoRecibido { get; set; }
        public decimal tasa { get; set; }
        public string lote { get; set; }
        public string referencia { get; set; }
        public Enumerados.enumTipoMedioCobro tipoMedioCobro { get; set; }


        public Pago()
        {
            codigo = "";
            descripcion = "";
            importe = 0.0m;
            montoRecibido = 0.0m;
            tasa = 0.0m;
            lote = "";
            referencia = "";
            tipoMedioCobro = Enumerados.enumTipoMedioCobro.SinDefinir;
        }
    }

}
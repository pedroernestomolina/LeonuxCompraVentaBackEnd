using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Reporte.Pago
{

    public class Enumerados
    {

        public enum enumEstatus { SinDefinir = -1, Activo = 1, Anulado };
        public enum enumTipoDocumento { SinDefinir = -1, Factura = 1, NotaCredito };
        public enum enumTipoMedioCobro { SinDefinir = -1, Efectivo = 1, Divisa, Electronico, Otro };

    }

}
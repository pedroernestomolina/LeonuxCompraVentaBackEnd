using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Venta
{

    public class Enumerados
    {

        public enum enumTipoDocumento { SinDefinir = -1, Factura = 1, NotaDebito, NotaCredito, NotaEntrega, Pedido, Presupuesto };
        public enum enumCondicionPago { sinDefinir = -1, Contado = 1, Credito };

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Venta
{
    
    public class AgregarFormaPago
    {

        public string AutoMedioPago { get; set; }
        public string AutoAgencia { get; set; }

        public string MedioPagoCodigo { get; set; }
        public string MedioPagoDescripcion { get; set; }
        public string AgenciaDescripcion { get; set; }

        public decimal MontoImporte { get; set; }
        public decimal MontoRecibido { get; set; }
        public string NumeroRef { get; set; }
        public string Lote { get; set; }
        public string Referencia { get; set; }

    }

}
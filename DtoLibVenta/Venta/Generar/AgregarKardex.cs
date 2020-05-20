using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Venta
{

    public class AgregarKardex
    {

        public string AutoProducto { get; set; }
        public string AutoDeposito { get; set; }
        public string AutoConcepto { get; set; }
        public string Entidad { get; set; }
        public decimal CostoPromedioUnd { get; set; }
        public decimal PrecioUnd { get; set; }
        public decimal CantidadUnd { get; set; }
        public decimal Cantidad { get; set; }
        public decimal TotalCostoVenta { get; set; }
        public int Signo { get; set; }
        public string Codigo { get; set; }
        public string Siglas { get; set; }
        public string Notas { get; set; }
        public string Modulo { get; set; }

    }

}
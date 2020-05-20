using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Venta
{

    public class AgregarTotal
    {

        public decimal VentaNeta { get; set; }
        public decimal CostoVenta { get; set; }
        public decimal Utilidad { get; set; }
        public decimal UtilidadPorc { get; set; }

        public decimal SubTotalNeto { get; set; }
        public decimal DescuentoMonto_1 { get; set; }
        public decimal DescuentoMonto_2 { get; set; }
        public decimal DescuentoPorct_1 { get; set; }
        public decimal DescuentoPorct_2 { get; set; }
        public decimal CargoMonto { get; set; }
        public decimal CargoPorct { get; set; }

        public decimal SubTotal { get; set; }
        public decimal SubTotalImpuesto { get; set; }

        public decimal MontoTotal { get; set; }
        public decimal MontoExento { get; set; }
        public decimal MontoBase { get; set; }
        public decimal MontoImpuesto { get; set; }
        public decimal MontoBase1 { get; set; }
        public decimal MontoBase2 { get; set; }
        public decimal MontoBase3 { get; set; }
        public decimal MontoImp1 { get; set; }
        public decimal MontoImp2 { get; set; }
        public decimal MontoImp3 { get; set; }
        public decimal Tasa1 { get; set; }
        public decimal Tasa2 { get; set; }
        public decimal Tasa3 { get; set; }

        public decimal SaldoPendiente { get; set; }
        public decimal MontoDivisa { get;set; }

    }

}
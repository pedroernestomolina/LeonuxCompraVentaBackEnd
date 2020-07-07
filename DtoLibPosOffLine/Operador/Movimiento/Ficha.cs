using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Operador.Movimiento
{
    
    public class Ficha
    {

        public int cntFactura { get; set; }
        public int cntNDebito { get; set; }
        public int cntNCredito { get; set; }
        public decimal montoFactura { get; set; }
        public decimal montoNDebito { get; set; }
        public decimal montoNCredito { get; set; }

        public decimal montoEfectivo { get; set; }

        public decimal cntDivisa { get; set; }
        public decimal montoDivisa { get; set; }

        public decimal cntElectronico { get; set; }
        public decimal montoElectronico { get; set; }

        public decimal cntOtros { get; set; }
        public decimal montoOtros { get; set; }

    }

}
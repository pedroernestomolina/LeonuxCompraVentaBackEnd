using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Operador.Cerrar
{

    public class Movimiento
    {

        public decimal diferencia { get; set; }
        public decimal efectivo { get; set; }
        public decimal divisa { get; set; }
        public decimal cntdivisa { get; set; }
        public decimal tarjeta { get; set; }
        public decimal otros { get; set; }
        public decimal firma { get; set; }
        public decimal devolucion { get; set; }
        public decimal subTotal { get; set; }
        public decimal total { get; set; }
        public decimal mEfectivo { get; set; }
        public decimal mDivisa { get; set; }
        public decimal mCntDivisa { get; set; }
        public decimal mTarjeta { get; set; }
        public decimal mOtro { get; set; }
        public decimal mFirma { get; set; }
        public decimal mSubTotal { get; set; }
        public decimal mTotal { get; set; }

    }

}
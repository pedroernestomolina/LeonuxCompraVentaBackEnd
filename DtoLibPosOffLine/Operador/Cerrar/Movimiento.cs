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
        public decimal tarjeta { get; set; }
        public decimal otros { get; set; }
        public decimal firma { get; set; }
        public decimal devolucion { get; set; }
        public decimal subTotal { get; set; }
        public decimal total { get; set; }
        public decimal mEfectivo { get; set; }
        public decimal mDivisa { get; set; }
        public decimal mTarjeta { get; set; }
        public decimal mOtro { get; set; }
        public decimal mFirma { get; set; }
        public decimal mSubTotal { get; set; }
        public decimal mTotal { get; set; }
        //
        public decimal cntDivisa { get; set; }
        public decimal cntDivisaUsu { get; set; }
        public int cntDoc { get; set; }
        public int cntDocFac { get; set; }
        public int cntDocNcr { get; set; }
        public decimal montoFac { get; set; }
        public decimal montoNcr { get; set; }


        public Movimiento() 
        {
            diferencia = 0.0m;
            efectivo = 0.0m;
            divisa = 0.0m;
            tarjeta = 0.0m;
            otros = 0.0m;
            firma = 0.0m;
            devolucion = 0.0m;
            subTotal = 0.0m;
            total = 0.0m;
            mEfectivo = 0.0m;
            mDivisa = 0.0m;
            mTarjeta = 0.0m;
            mOtro = 0.0m;
            mFirma = 0.0m;
            mSubTotal = 0.0m;
            mTotal = 0.0m;
            cntDivisa = 0.0m;
            cntDivisaUsu = 0.0m;
            cntDoc = 0;
            cntDocFac = 0;
            cntDocNcr = 0;
            montoFac = 0.0m;
            montoNcr = 0.0m;
        }

    }

}
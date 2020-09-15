using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Servidor.RecogerDataEnviar
{

    public class MovimientoCierre
    {

        public string autoUsuario { get; set; }
        public string codigoUsuario { get; set; }
        public string usuario { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string prefijo { get; set; }

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

    }

}
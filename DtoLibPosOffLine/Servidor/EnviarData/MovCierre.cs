using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Servidor.EnviarData
{
    
    public class MovCierre
    {

        public string autoUsuario { get; set; }
        public string codigoUsuario { get; set; }
        public string usuario { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public decimal diferencia { get; set; }
        public decimal efectivo { get; set; }
        public decimal divisa { get; set; }
        public decimal debito { get; set; }
        public decimal otros { get; set; }
        public decimal firma { get; set; }
        public decimal devolucion { get; set; }
        public decimal subTotal { get; set; }
        public decimal total { get; set; }
        public decimal mfectivo { get; set; }
        public decimal mdivisa { get; set; }
        public decimal mdebito { get; set; }
        public decimal motros { get; set; }
        public decimal mfirma { get; set; }
        public decimal msubtotal { get; set; }
        public decimal mtotal { get; set; }

    }

}
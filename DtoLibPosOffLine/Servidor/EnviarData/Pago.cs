using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Servidor.EnviarData
{
    
    public class CxCPago
    {

        public CxC Pago { get; set; }
        public CxCRecibo Recibo { get; set; }
        public CxCDocumento Documento { get; set; }
        public List<CxCMetodoPago> MetodoPago { get; set; }

    }

}

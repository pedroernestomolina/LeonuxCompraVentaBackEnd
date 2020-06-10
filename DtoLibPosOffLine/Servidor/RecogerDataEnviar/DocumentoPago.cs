using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Servidor.RecogerDataEnviar
{
    
    public class DocumentoPago
    {

        public int Id { get; set; }
        public string AutoMedioCobro { get; set; }
        public string CodigoMedioCobro { get; set; }
        public string MedioCobro { get; set; }
        public decimal MontoImporte { get; set; }
        public decimal MontoRecibido { get; set; }
        public decimal Tasa { get; set; }
        public string LoteNro { get; set; }
        public string ReferenciaNro { get; set; }

    }

}
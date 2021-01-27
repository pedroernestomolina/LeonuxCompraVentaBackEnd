using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Corrector.Factura
{
    
    public class Ficha
    {

        public string autoDoc { get; set; }
        public string autoProveedor { get; set; }
        public string documentoNro { get; set; }
        public DateTime fechaDocumento { get; set; }
        public string nombreRazonSocialProveedor { get; set; }
        public string direccionFiscalProveedor { get; set; }
        public string ciRifProveedor { get; set; }
        public string notaDocumento { get; set; }
        public string controlNro { get; set; }

    }

}
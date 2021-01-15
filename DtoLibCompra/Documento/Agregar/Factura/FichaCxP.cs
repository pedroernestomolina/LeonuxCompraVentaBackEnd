using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Agregar.Factura
{
    
    public class FichaCxP
    {

        public string tipoDocumento { get; set; }
        public string documentoNro { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public string nota { get; set; }
        public decimal importe { get; set; }
        public decimal acumulado { get; set; }
        public string autoProveedor { get; set; }
        public string nombreRazonSocialProveedor { get; set; }
        public string ciRifProveedor { get; set; }
        public string codigoProveedor { get; set; }
        public string esCancelado { get; set; }
        public decimal resta { get; set; }
        public string esAnulado { get; set; }
        public string numero { get; set; }
        public string autoAgencia { get; set; }
        public string nombreAgencia { get; set; }
        public int signoDocumento { get; set; }
        public int diasCredito { get; set; }
        public string Anexo { get; set; }
        public string estatusCierreContable { get; set; }
        public decimal importeDivisa { get; set; }

    }

}
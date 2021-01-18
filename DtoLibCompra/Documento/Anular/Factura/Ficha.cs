using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Anular.Factura
{
    
    public class Ficha
    {

        public string autoDocumento { get; set; }
        public string codigoDocumento { get; set; }
        public FichaAuditoria auditoria { get; set; }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Cliente
{
    
    public class PendientePorCobrar
    {

        public string AutoDocumento { get; set; }
        public Enumerados.enumTipoDocumentoPorCobrar TipoDocumento { get; set; }
        public string Documento { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal MontoDocumento { get; set; }
        public decimal Abonado { get; set; }
        public int Signo { get; set; }
        public bool IsAdministrativo { get; set; }

    }

}
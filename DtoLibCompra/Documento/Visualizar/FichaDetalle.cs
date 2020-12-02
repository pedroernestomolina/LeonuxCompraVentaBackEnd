using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLibCompra.Documento.Visualizar
{

    public class FichaDetalle
    {

        public string prdCodigo { get; set; }
        public string prdNombre { get; set; }
        public decimal cntFactura { get; set; }
        public string empaqueCompra { get; set; }
        public int contenido { get; set; }
        public decimal precioFactura { get; set; }
        public decimal dscto1p { get; set; }
        public decimal dscto2p { get; set; }
        public decimal dscto3p { get; set; }
        public decimal dscto1m { get; set; }
        public decimal dscto2m { get; set; }
        public decimal dscto3m { get; set; }
        public decimal tasaIva { get; set; }
        public decimal importe { get; set; }
        public string depositoCodigo { get; set; }
        public string depositoNombre { get; set; }

    }

}
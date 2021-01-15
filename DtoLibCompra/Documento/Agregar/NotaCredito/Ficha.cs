using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Agregar.NotaCredito
{
    
    public class Ficha
    {

        public FichaDocumento documento { get; set; }
        public List<FichaDetalle> detalles { get; set; }
        public FichaCxP cxp { get; set; }
        public List<FichaPrdDeposito> prdDeposito { get; set; }
        public List<FichaPrdKardex> prdKardex { get; set; }

    }

}
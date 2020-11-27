using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Cargar.Factura
{
    
    public class Ficha
    {

        public FichaDocumento documento { get; set; }
        public List<FichaDetalle> detalles { get; set; }
        public List<FichaPrdDeposito> prdDeposito { get; set; }
        public List<FichaPrdKardex> prdKardex { get; set; }
        public List<FichaPrdCosto> prdCosto { get; set; }
        public List<FichaPrdProveedor> prdProveedor { get; set; }
        public List<FichaPrdCostoHistorico> prdCostosHistorico { get; set; }
        public List<FichaPrdPrecio> prdPrecios { get; set; }
        public List<FichaPrdPrecioHistorico> prdPreciosHistorico { get; set; }
        public FichaCxP cxp { get; set; }

    }

}
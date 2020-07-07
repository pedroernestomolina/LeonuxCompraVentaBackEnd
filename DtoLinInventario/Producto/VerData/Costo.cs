using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.VerData
{
    
    public class Costo
    {

        public decimal costoProveedor { get; set; }
        public decimal costoImportacion { get; set; }
        public decimal costoVario { get; set; }
        public decimal costoDivisa { get; set; }
        public decimal costo { get; set; }
        public decimal costoPromedio { get; set; }
        public DateTime fechaUltCambio { get; set; }

    }

}
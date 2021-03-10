using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Proveedor.Articulos
{
    
    public class Ficha
    {

        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public DateTime fecha { get; set; }
        public string documento { get; set; }
        public decimal cantidad { get; set; }
        public decimal cantUnd { get; set; }
        public string empaque { get; set; }
        public string estatus { get; set; }
        public int contenidoEmp { get; set; }
        public string codTipoDoc { get; set; }
        public string nombreTipoDoc { get; set; }
        public string serie { get; set; }
        public decimal tasaCambio { get; set; }
        public decimal costo { get; set; }
        public decimal costoUnd { get; set; }
        public int signo { get; set; }

    }

}
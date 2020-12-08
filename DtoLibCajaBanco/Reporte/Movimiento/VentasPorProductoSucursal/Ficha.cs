using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCajaBanco.Reporte.Movimiento.VentasPorProductoSucursal
{
    
    public class Ficha
    {

        public string codigoSuc { get; set; }
        public string nombreSuc { get; set; }
        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public decimal cantidad { get; set; }
        public decimal totalMonto { get; set; }
        public decimal totalMontoDivisa { get; set; }
        public string nombreDocumento { get; set; }
        public int signo { get; set; }

    }

}
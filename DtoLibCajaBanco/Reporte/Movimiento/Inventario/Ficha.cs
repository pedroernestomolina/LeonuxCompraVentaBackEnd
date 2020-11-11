using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCajaBanco.Reporte.Movimiento.Inventario
{
    
    public class Ficha
    {

        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public decimal? entradas { get; set; }
        public decimal? entradasOt { get; set; }
        public decimal? salidas { get; set; }
        public string decimales { get; set; }
        public decimal? tEntradas { get; set; }
        public decimal? tSalidas { get; set; }

    }

}
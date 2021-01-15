using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCajaBanco.Reporte.Movimiento.CobranzaDiaria
{
    
    public class Ficha
    {

        public List<Data> data { get; set; }
        public List<Movimiento> movimiento { get; set; }
        public decimal montoCredito { get; set; }

    }

}
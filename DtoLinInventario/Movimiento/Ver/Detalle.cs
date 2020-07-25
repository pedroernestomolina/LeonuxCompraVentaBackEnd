using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Ver
{
    
    public class Detalle
    {

        public string codigo { get; set; }
        public string descripcion { get; set; }
        public decimal cantidad { get; set; }
        public int signo { get; set; }
        public decimal costoUnd { get; set; }
        public decimal importe { get; set; }

    }

}
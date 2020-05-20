using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Configuracion.MedioCobro
{
    
    public class Ficha
    {

        public Medio Efectivo { get; set; }
        public Medio Divisa { get; set; }
        public Medio Electronico { get; set; }
        public Medio Otro { get; set; }

    }

}

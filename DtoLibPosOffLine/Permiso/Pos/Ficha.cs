using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Permiso.Pos
{

    public class Ficha
    {

        public Permiso Devolucion { get; set; }
        public Permiso AnularVenta { get; set; }
        public Permiso Sumar { get; set; }
        public Permiso Multiplicar { get; set; }
        public Permiso Restar { get; set; }
        public Permiso DejarCtaPendiente { get; set; }
        public Permiso AnularCtaPendiente { get; set; }
        public Permiso DarDesctoGlobal { get; set; }
        public Permiso CtaCredito { get; set; }

    }

}
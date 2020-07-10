using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Operador.Cerrar
{

    public class Ficha
    {

        public int IdOperador { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Estatus { get; set; }
        public Movimiento Movimientos { get; set; }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Servidor.EnviarData
{
    
    public class Ficha
    {

        public List<Jornada> Jornadas { get; set; }
        public List<DateTime> FechasMov { get; set; }
        public List<Movimiento> Movimientos { get; set; }
        public List<Serie> Series { get; set; }

    }

}
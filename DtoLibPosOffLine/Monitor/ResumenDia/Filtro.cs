using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Monitor.ResumenDia
{
    
    public class Filtro
    {

        public string equipo { get; set; }
        public int idOperador { get; set; }


        public Filtro() 
        {
            idOperador = -1;
            equipo = "";
        }

    }

}
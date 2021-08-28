using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.ResumenVentaPos.Generar
{
    
    public class Filtro
    {

        public int idOperador { get; set; }
        public string sucEquipo { get; set; }


        public Filtro() 
        {
            idOperador = -1;
            sucEquipo = "";
        }

    }

}

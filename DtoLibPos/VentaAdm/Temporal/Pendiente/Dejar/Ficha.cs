using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Pendiente.Dejar
{
    
    public class Ficha
    {

        public int idTemporal { get; set; }
        public string notas { get; set; }


        public Ficha() 
        {
            idTemporal = -1;
            notas = "";
        }

    }

}
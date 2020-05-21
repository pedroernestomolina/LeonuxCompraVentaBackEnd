using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Permiso.Pos
{
    
    public class Permiso
    {

        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool RequiereClave { get; set; }


        public Permiso()
        {
            Codigo = "";
            Descripcion = "";
            RequiereClave = false;
        }

    }

}
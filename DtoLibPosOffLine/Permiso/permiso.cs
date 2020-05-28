using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Permiso
{
    
    public class permiso
    {

        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool RequiereClave { get; set; }


        public permiso()
        {
            Codigo = "";
            Descripcion = "";
            RequiereClave = false;
        }

    }

}
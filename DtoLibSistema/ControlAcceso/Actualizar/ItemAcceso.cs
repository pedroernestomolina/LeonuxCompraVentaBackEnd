using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.ControlAcceso.Actualizar
{
    
    public class ItemAcceso
    {

        public string codGrupo { get; set; }
        public string codFuncion { get; set; }
        public string estatus { get; set; }
        public string seguridad { get; set; }


        public ItemAcceso() 
        {
            codGrupo = "";
            codFuncion = "";
            estatus = "";
            seguridad = "";
        }

    }

}
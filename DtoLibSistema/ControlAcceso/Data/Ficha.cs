using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.ControlAcceso.Data
{
    
    public class Ficha
    {

        public string fNombre { get; set; }
        public string fCodigo { get; set; }
        public string seguridad { get; set; }
        public string estatus { get; set; }


        public Ficha()
        {
            fNombre = "";
            fCodigo = "";
            seguridad = "";
            estatus = "";
        }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.SerieFiscal.Lista
{
    
    public class Ficha
    {

        public string id { get; set; }
        public string serie { get; set; }
        public int correlativo { get; set; }
        public string control { get; set; }
        public string estatus { get; set; }


        public Ficha() 
        {
            id = "";
            serie = "";
            control = "";
            correlativo = 0;
            estatus = "";
        }

    }

}
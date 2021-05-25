using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Lista
{
    
    public class Filtro
    {

        public class Fecha
        {
            public DateTime desde { get; set; }
            public DateTime hasta { get; set; }


            public Fecha()
            {
                desde = DateTime.Now.Date;
                hasta = DateTime.Now.Date;
            }
        }


        public string idArqueo { get; set; }
        public Fecha fecha { get;set;}
        public string codSucursal { get; set; }
        public string codTipoDocumento { get; set; }


        public Filtro()
        {
            idArqueo = "";
            fecha = null;
            codSucursal = "";
            codTipoDocumento = "";
        }

    }

}
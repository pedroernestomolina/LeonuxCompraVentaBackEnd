using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Lista
{

    public class Filtro 
    {

        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public string CodigoSuc { get; set; }
        public Enumerados.enumTipoDocumento TipoDocumento { get; set; }

        public Filtro()
        {
            Desde = DateTime.Now.Date;
            Hasta = DateTime.Now.Date;
            CodigoSuc = "";
            TipoDocumento = Enumerados.enumTipoDocumento.SinDefinir;
        }

    }

}
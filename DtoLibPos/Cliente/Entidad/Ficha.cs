using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Cliente.Entidad
{
    
    public class Ficha
    {

        public string Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string CiRif { get; set; }
        public string DireccionFiscal { get; set; }
        public string Telefono { get; set; }
        public string estatus { get; set; }


        public Ficha()
        {
            Id = "";
            Codigo = "";
            Nombre = "";
            CiRif = "";
            DireccionFiscal = "";
            Telefono = "";
            estatus = "";
        }

    }

}
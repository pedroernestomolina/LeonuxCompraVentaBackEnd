using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.Negocio.Editar
{
    
    public class Ficha
    {

        public string auto { get; set; }
        public string nombre { get; set; }
        public string rif { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string contacto { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string webSite { get; set; }
        public string pais { get; set; }
        public string estado { get; set; }
        public string ciudad { get; set; }
        public string codPostal { get; set; }


        public Ficha() 
        {
            auto = "";
            nombre = "";
            rif = "";
            direccion = "";
            telefono = "";
            contacto = "";
            fax = "";
            email = "";
            webSite = "";
            pais = "";
            estado = "";
            ciudad = "";
            codPostal = "";
        }

    }

}
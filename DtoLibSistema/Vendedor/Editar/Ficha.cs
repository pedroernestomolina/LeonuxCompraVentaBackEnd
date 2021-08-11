using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.Vendedor.Editar
{
    
    public class Ficha
    {

        public string id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string ciRif { get; set; }
        public string direccion { get; set; }
        public string contacto { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string webSite { get; set; }
        public string advertencia { get; set; }
        public string memo { get; set; }


        public Ficha() 
        {
            id = "";
            codigo = "";
            nombre = "";
            ciRif = "";
            direccion = "";
            contacto = "";
            telefono = "";
            email = "";
            webSite = "";
            advertencia = "";
            memo = "";
        }

    }

}
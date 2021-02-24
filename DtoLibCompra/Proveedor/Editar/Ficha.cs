using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Proveedor.Editar
{
    
    public class Ficha
    {

        public string autoPrv { get; set; }
        public string idGrupo { get; set; }
        public string idEstado { get; set; }
        public string codigo { get; set; }
        public string razonSocial { get; set; }
        public string ciRif { get; set; }
        public string dirFiscal { get; set; }
        public string contacto { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string webSite { get; set; }
        public string pais { get; set; }
        public string denFiscal { get; set; }
        public string codPostal { get; set; }
        public decimal retIva { get; set; }


        public Ficha()
        {
            autoPrv = "";
            idGrupo = "";
            idEstado = "";
            codigo = "";
            razonSocial = "";
            ciRif = "";
            dirFiscal = "";
            contacto = "";
            telefono = "";
            email = "";
            webSite = "";
            pais = "";
            codPostal = "";
            retIva = 0.0m;
        }

    }

}
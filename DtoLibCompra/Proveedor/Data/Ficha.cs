using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Proveedor.Data
{
    
    public class Ficha
    {

        public string autoId { get; set; }
        public string autoEstado { get; set; }
        public string autoGrupo { get; set; }
        public string codigo { get; set; }
        public string ciRif { get; set; }
        public string nombreRazonSocial { get; set; }
        public string direccionFiscal { get; set; }
        public string telefono { get; set; }
        public bool isActivo { get; set; }
        public string nombreEstado { get; set; }
        public string nombreGrupo { get; set; }
        public string nombreContacto { get; set; }

    }

}
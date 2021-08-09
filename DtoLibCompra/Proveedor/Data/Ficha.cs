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
        public string email { get; set; }
        public string website { get; set; }
        public string pais { get; set; }
        public string codigoPostal { get; set; }
        public decimal retIva { get; set; }
        public string denominacionFiscal { get; set; }
        public DateTime fechaAlta { get; set; }
        public DateTime fechaUltCompra { get; set; }
        public DateTime fechaBaja { get; set; }

    }

}
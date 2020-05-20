using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Venta
{
    
    public class AgregarCxc
    {

        public string AutoCliente {get;set;}
        public string AutoVendedor {get;set;}
        public decimal MontoImporteTotal { get; set; }
        public decimal MontoImporteNeto { get; set; }
        public decimal MontoAcumulado {get;set;} 
        public decimal MontoResta {get;set;} 
        public string ClienteNombre {get;set;}
        public string ClienteCiRif {get;set;}
        public string ClienteCodigo {get;set;}
        public bool IsCancelado {get;set;}
        public int Signo {get;set;}
        public string Notas { get; set; }
        public string DocumentoVentaTipo { get; set; }
        public string DocumentoVentaSerie { get; set; }

    }

}
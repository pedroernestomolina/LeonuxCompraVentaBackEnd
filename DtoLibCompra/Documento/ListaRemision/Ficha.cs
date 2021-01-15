using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.ListaRemision
{
    
    public class Ficha
    {

        public string auto { get; set; }
        public DateTime fechaEmision { get; set; }
        public string docNro { get; set; }
        public string control{ get; set; }
        public string docNombre { get; set; }
        public string docTipo { get; set; }
        public decimal total { get; set; }
        public decimal montoDivisa { get; set; }

    }

}
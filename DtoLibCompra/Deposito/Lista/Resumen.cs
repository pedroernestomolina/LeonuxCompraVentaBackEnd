using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Deposito.Lista
{
    
    public class Resumen
    {

        public string id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string codigoSuc { get; set; }


        public Resumen()
        {
            id = "";
            codigo = "";
            nombre = "";
            codigoSuc = "";
        }

    }

}
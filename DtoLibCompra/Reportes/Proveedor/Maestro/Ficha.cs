using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Reportes.Proveedor.Maestro
{
    
    public class Ficha
    {

        public string codigo { get; set; }
        public string nombre { get; set; }
        public string ciRif { get; set; }
        public string telefono { get; set; }
        public string dirFiscal { get; set; }
        public string estatus { get; set; }


        public Ficha()
        {
            codigo = "";
            nombre = "";
            ciRif = "";
            telefono = "";
            dirFiscal = "";
            estatus = "";
        }

    }

}
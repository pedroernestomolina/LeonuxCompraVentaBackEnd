using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.Deposito
{

    public class Resumen
    {

        public string auto { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string codigoSucursal { get; set; }
        public string sucursal { get; set; }
        public string estatusDep { get; set; }


        public Resumen() 
        {
            auto = "";
            codigo = "";
            nombre = "";
            codigoSucursal = "";
            sucursal = "";
            estatusDep = "";
        }

    }

}
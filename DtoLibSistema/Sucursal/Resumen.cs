using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.Sucursal
{
    
    public class Resumen
    {

        public string auto { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string grupo { get; set; }
        public string deposito { get; set; }
        public string estatusFactMayor { get; set; }


        public Resumen() 
        {
            auto = "";
            codigo = "";
            nombre = "";
            grupo = "";
            deposito = "";
            estatusFactMayor = "";
        }

    }

}
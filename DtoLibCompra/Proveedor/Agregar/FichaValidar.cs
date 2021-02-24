using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Proveedor.Agregar
{
    
    public class FichaValidar
    {

        public string codigo { get; set; }
        public string razonSocial { get; set; }


        public FichaValidar()
        {
            codigo = "";
            razonSocial = "";
        }

    }

}
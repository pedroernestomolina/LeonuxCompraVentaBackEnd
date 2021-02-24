using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Proveedor.Editar
{
    
    public class FichaValidar
    {

        public string autoId { get; set; }
        public string codigo { get; set; }
        public string razonSocial { get; set; }


        public FichaValidar()
        {
            autoId = "";
            codigo = "";
            razonSocial = "";
        }

    }

}
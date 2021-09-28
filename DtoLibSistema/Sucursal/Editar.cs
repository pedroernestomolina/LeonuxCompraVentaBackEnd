using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.Sucursal
{
    
    public class Editar
    {

        public string auto { get; set; }
        public string autoGrupo { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string estatusFactMayor { get; set; }


        public Editar() 
        {
            auto = "";
            autoGrupo = "";
            codigo = "";
            nombre = "";
            estatusFactMayor = "";
        }

    }

}
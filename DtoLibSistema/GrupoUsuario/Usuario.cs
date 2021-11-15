using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.GrupoUsuario
{
    
    public class Usuario
    {

        public string autoId { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string codigo { get; set; }
        public string estatus { get; set; }


        public Usuario() 
        {
            autoId = "";
            nombre="";
            apellido = "";
            codigo = "";
            estatus = "";
        }

    }

}
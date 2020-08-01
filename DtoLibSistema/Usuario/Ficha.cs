using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.Usuario
{
    
    public class Ficha
    {

        public string auto { get; set; }
        public string autoGrupo { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string codigo { get; set; }
        public string clave { get; set; }
        public Enumerados.EnumModo estatus { get; set; }
        public DateTime fechaAlta { get; set; }
        public DateTime? fechaBaja { get; set; }
        public DateTime? fechaUltSesion { get; set; }
        public string grupo { get; set; }

    }

}
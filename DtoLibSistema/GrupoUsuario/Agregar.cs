using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.GrupoUsuario
{

    public class Agregar
    {

        public string nombre { get; set; }
        public List<Permiso> permisos { get; set; }
    }

}

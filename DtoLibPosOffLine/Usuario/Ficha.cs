using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Usuario
{
    
    public class Ficha
    {

        public enum enumEstatus { SinDefinir = -1, Activo = 1, Inactivo };

        public string UsuarioAuto { get; set; }
        public string UsuarioClave { get; set; }
        public string UsuarioCodigo { get; set; }
        public string UsuarioDescripcion { get; set; }
        public string GrupoAuto { get; set; }
        public string GrupoDescripcion { get; set; }
        public enumEstatus UsuarioEstatus { get; set; }

    }

}
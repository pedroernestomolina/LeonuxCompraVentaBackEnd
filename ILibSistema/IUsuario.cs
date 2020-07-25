using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibSistema
{
    
    public interface IUsuario
    {

        DtoLib.ResultadoEntidad<DtoLibSistema.Usuario.Ficha> Usuario_Principal();

    }

}
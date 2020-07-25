using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IUsuario
    {

        DtoLib.ResultadoEntidad<DtoLibInventario.Usuario.Ficha> Usuario_Principal();

    }

}
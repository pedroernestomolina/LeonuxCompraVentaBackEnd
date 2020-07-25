using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCajaBanco
{

    public interface IUsuario
    {

        DtoLib.ResultadoEntidad<DtoLibCajaBanco.Usuario.Ficha> Usuario_Principal();

    }

}
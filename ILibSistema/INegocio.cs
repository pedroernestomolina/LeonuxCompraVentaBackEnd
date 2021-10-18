using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibSistema
{
    
    public interface INegocio
    {

        DtoLib.ResultadoEntidad<DtoLibSistema.Negocio.Entidad.Ficha> Negocio_GetEntidad_ByAuto(string autoEmpresa);
        DtoLib.Resultado Negocio_EditarFicha(DtoLibSistema.Negocio.Editar.Ficha ficha);

    }

}
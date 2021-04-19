using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    
    public interface IPermiso
    {

        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_IngresarPos(string idGrupoUsu);
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Pos(string idGrupoUsu, string codFuncion);

    }

}
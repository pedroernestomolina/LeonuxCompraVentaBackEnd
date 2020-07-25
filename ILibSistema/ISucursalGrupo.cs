using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibSistema
{
    
    public interface ISucursalGrupo
    {

        DtoLib.ResultadoLista<DtoLibSistema.GrupoSucursal.Resumen> SucursalGrupo_GetLista();
        DtoLib.ResultadoEntidad<DtoLibSistema.GrupoSucursal.Ficha> SucursalGrupo_GetFicha(string auto);
        DtoLib.ResultadoAuto SucursalGrupo_Agregar(DtoLibSistema.GrupoSucursal.Agregar ficha);
        DtoLib.Resultado SucursalGrupo_Editar (DtoLibSistema.GrupoSucursal.Editar ficha);

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibSistema
{
    
    public interface ISucursal
    {

        DtoLib.ResultadoLista<DtoLibSistema.Sucursal.Resumen> Sucursal_GetLista();
        DtoLib.ResultadoEntidad<DtoLibSistema.Sucursal.Ficha> Sucursal_GetFicha(string auto);
        DtoLib.ResultadoAuto Sucursal_Agregar(DtoLibSistema.Sucursal.Agregar ficha);
        DtoLib.Resultado Sucursal_Editar(DtoLibSistema.Sucursal.Editar ficha);
        DtoLib.Resultado Sucursal_AsignarDepositoPrincipal(DtoLibSistema.Sucursal.AsignarDepositoPrincipal ficha);
        DtoLib.Resultado Sucursal_QuitarDepositoPrincipal(string autoSuc);
        DtoLib.ResultadoEntidad<int> Sucursal_GeneraCodigoAutomatico();

    }

}
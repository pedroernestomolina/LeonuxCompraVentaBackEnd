using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    
    public interface IConfiguracion
    {

        DtoLib.ResultadoEntidad<decimal> Configuracion_FactorDivisa();
        DtoLib.Resultado Configuracion_Pos_Actualizar(DtoLibPos.Configuracion.Actualizar.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.Entidad.Ficha> Configuracion_Pos_GetFicha();

    }

}
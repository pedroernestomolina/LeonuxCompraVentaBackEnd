using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{
    
    public interface IPermiso
    {

        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Permiso.Pos.Ficha> Permiso_ManejoPos();
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Permiso.AdmDocumento.Ficha> Permiso_AdmDocumento();

        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Permiso.Actual.Ficha> Permiso_ActualCargar();
        DtoLib.Resultado Permiso_Actualizar(DtoLibPosOffLine.Permiso.Actualizar.Ficha ficha);

    }

}
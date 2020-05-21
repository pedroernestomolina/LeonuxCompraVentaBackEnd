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

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibSistema
{
    
    public interface IControlAcceso
    {

        DtoLib.ResultadoLista<DtoLibSistema.ControlAcceso.Data.Ficha> ControlAcceso_GetData(string idGrupo);
        DtoLib.Resultado ControlAcceso_Actualizar(DtoLibSistema.ControlAcceso.Actualizar.Ficha ficha);

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    
    public interface ISistema
    {

        DtoLib.ResultadoEntidad<DtoLibPos.Sistema.TipoDocumento.Entidad.Ficha> Sistema_TipoDocumento_GetFichaById(string id);

        DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Serie.Entidad.Ficha> Sistema_Serie_GetFichaById(string id);

    }

}
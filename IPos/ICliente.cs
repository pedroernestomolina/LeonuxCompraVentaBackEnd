using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    
    public interface ICliente
    {

        DtoLib.ResultadoLista<DtoLibPos.Cliente.Lista.Ficha> Cliente_GetLista(DtoLibPos.Cliente.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Cliente.Entidad.Ficha> Cliente_GetFichaById(string id);
        DtoLib.ResultadoEntidad<DtoLibPos.Cliente.Entidad.Ficha> Cliente_GetFichaByCiRif(string ciRif);
        DtoLib.ResultadoAuto Cliente_Agregar(DtoLibPos.Cliente.Agregar.Ficha ficha);

    }

}
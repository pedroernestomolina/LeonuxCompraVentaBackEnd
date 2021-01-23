using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCompras
{

    public interface IGrupo
    {

        DtoLib.ResultadoLista<DtoLibCompra.Maestros.Grupo.Resumen> Grupo_GetLista();
        DtoLib.ResultadoEntidad<DtoLibCompra.Maestros.Grupo.Ficha> Grupo_GetFicha(string auto);
        DtoLib.ResultadoAuto Grupo_Agregar(DtoLibCompra.Maestros.Grupo.Agregar ficha);
        DtoLib.Resultado Grupo_Editar(DtoLibCompra.Maestros.Grupo.Editar ficha);

    }

}
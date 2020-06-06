using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{

    public interface IJornada
    {

        DtoLib.ResultadoId Jornada_Abrir(DtoLibPosOffLine.Jornada.Abrir.Ficha ficha);
        DtoLib.Resultado Jornada_Cerrar (DtoLibPosOffLine.Jornada.Cerrar.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Jornada.Cargar.Ficha> Jornada_Cargar(int idJornada);
        DtoLib.ResultadoEntidad<int> Jornada_Activa();
        DtoLib.ResultadoEntidad<bool> Jornada_Abrir_Verifica();

    }

}
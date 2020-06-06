using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{
    
    public interface IOperador
    {

        DtoLib.ResultadoId Operador_Abrir(DtoLibPosOffLine.Operador.Abrir.Ficha ficha);
        DtoLib.Resultado Operador_Cerrar(DtoLibPosOffLine.Operador.Cerrar.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Operador.Cargar.Ficha> Operador_Cargar(int idOperador);
        DtoLib.ResultadoEntidad<int> Operador_Activa();
        DtoLib.ResultadoEntidad<bool> Operador_Abrir_VerificaIsOk();

    }

}

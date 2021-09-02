using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{
    
    public interface IMonitor
    {

        DtoLib.ResultadoLista<DtoLibPosOffLine.Monitor.ListaResumen.Ficha> Monitor_ListaResumen();
        DtoLib.ResultadoLista<DtoLibPosOffLine.Monitor.GenerarResumen.Ficha> Monitor_GenerarResumen(DtoLibPosOffLine.Monitor.GenerarResumen.Filtro filtro);
        DtoLib.Resultado Monitor_SubirResumen(DtoLibPosOffLine.Monitor.SubirResumen.Ficha ficha);
        DtoLib.Resultado Monitor_InsertarCierre(DtoLibPosOffLine.Monitor.InsertarCierre.Ficha ficha);
        DtoLib.Resultado Monitor_Resumen_Dia(DtoLibPosOffLine.Monitor.ResumenDia.Filtro ficha);

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{
    
    public interface IReporte
    {

        DtoLib.ResultadoLista<DtoLibPosOffLine.Reporte.Pago.Detalle.Ficha> Reporte_Pago_Detalle(DtoLibPosOffLine.Reporte.Pago.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibPosOffLine.Reporte.NCredito.Ficha> Reporte_NCredito(DtoLibPosOffLine.Reporte.NCredito.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibPosOffLine.Reporte.Pago.Resumen.Ficha> Reporte_Pago_Resumen(DtoLibPosOffLine.Reporte.Pago.Filtro filtro);

    }

}
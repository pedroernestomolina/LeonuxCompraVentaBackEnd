using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCajaBanco
{

    public interface IReporteMovimiento
    {

        void Reporte(DateTime fecha);
        DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.ArqueoCajaPos.Ficha> CajaBanco_ArqueoCajaPos(DtoLibCajaBanco.Reporte.Movimiento.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.Inventario.Ficha> Reporte_InventarioResumen(DtoLibCajaBanco.Reporte.Movimiento.Inventario.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.ResumenVenta.Ficha> Reporte_VentaResumen(DtoLibCajaBanco.Reporte.Movimiento.ResumenVenta.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.FacturaDetalle.Ficha> Reporte_VentaDetalle(DtoLibCajaBanco.Reporte.Movimiento.FacturaDetalle.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.VentasPorProducto.Ficha> Reporte_VentaPorProducto (DtoLibCajaBanco.Reporte.Movimiento.VentasPorProducto.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.ResumenVentaSucursal.Ficha> Reporte_ResumenVentaSucursal(DtoLibCajaBanco.Reporte.Movimiento.ResumenVentaSucursal.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.ResumenDiarioVentaSucursal.Ficha> Reporte_ResumenDiarioVentaSucursal(DtoLibCajaBanco.Reporte.Movimiento.ResumenDiarioVentaSucursal.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Habladores.Ficha> Reporte_Habladores(DtoLibCajaBanco.Reporte.Habladores.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.VentasPorProductoSucursal.Ficha> Reporte_VentaPorProductoSucursal(DtoLibCajaBanco.Reporte.Movimiento.VentasPorProductoSucursal.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibCajaBanco.Reporte.Movimiento.CobranzaDiaria.Ficha> Reporte_CobranzaDiara(DtoLibCajaBanco.Reporte.Movimiento.CobranzaDiaria.Filtro filtro);

        //ANALISIS
        DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Analisis.VentaPromedio.Ficha> Reporte_Analisis_VentaPromedio(DtoLibCajaBanco.Reporte.Analisis.VentaPromedio.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Analisis.VentaProducto.Ficha> Reporte_Analisis_VentaProducto (DtoLibCajaBanco.Reporte.Analisis.VentaProducto.Filtro filtro);

    }

}
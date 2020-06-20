using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{
    
    public interface IConfiguracion
    {

        DtoLib.ResultadoEntidad<decimal> Configuracion_FactorCambio();
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.ModoPos.Ficha> Configuracion_ModoPos();
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Repesaje.Ficha> Configuracion_Repesaje();
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Serie.Ficha> Configuracion_Serie();
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Sucursal.Ficha> Configuracion_Sucursal();
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Vendedor.Ficha> Configuracion_Vendedor();
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Deposito.Ficha> Configuracion_Deposito();
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Cobrador.Ficha> Configuracion_Cobrador();
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Transporte.Ficha> Configuracion_Transporte();
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.MedioCobro.Ficha> Configuracion_MedioCobro();
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.MovConceptoInv.Ficha> Configuracion_MovConceptoInv();
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.ClaveAcceso.Ficha> Configuracion_ClavePos();
        DtoLib.ResultadoEntidad<string> Configuracion_TarifaPrecio();
        DtoLib.ResultadoEntidad<bool> Configuracion_EtiquetarPrecioPorTipoNegocio();

        DtoLib.ResultadoEntidad<bool> Configuracion_ActivarBusquedaPorDescripcion();
        DtoLib.Resultado Configuracion_Actualizar(DtoLibPosOffLine.Configuracion.Guardar.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Configuracion.Actual.Ficha> Configuracion_ActualCargar();

    }

}
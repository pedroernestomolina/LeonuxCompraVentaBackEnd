using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibVentas
{

    public interface IConfiguracion
    {

        DtoLib.ResultadoEntidad<string> Clave_NivelMinimo();
        DtoLib.ResultadoEntidad<string> Clave_NivelMedio();
        DtoLib.ResultadoEntidad<string> Clave_NivelMaximo();
        DtoLib.ResultadoEntidad<decimal> FactorCambioDivisa();
        DtoLib.ResultadoEntidad<decimal> FactorCambioDivisaParaRecibir();
        DtoLib.ResultadoEntidad<DtoLibVenta.Sistema.Enumerados.enumPreferenciaBusquedaProducto> PreferenciaBusquedaProducto();
        DtoLib.ResultadoEntidad<DtoLibVenta.Sistema.Enumerados.enumPreferenciaBusquedaCliente> PreferenciaBusquedaCliente();
        DtoLib.ResultadoEntidad<DtoLibVenta.Sistema.Enumerados.enumMetodoCalculoUtilidad> MetodoCalculoUtilidad();
        DtoLib.ResultadoEntidad<bool> HabilitarRupturaPorExistencia();
        DtoLib.ResultadoEntidad<bool> HabilitarAlertaPorExistenciaEnNegativa();

    }

}
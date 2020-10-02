using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{

    public interface IConfiguracion
    {

        DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda> Configuracion_PreferenciaBusqueda();
        DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumMetodoCalculoUtilidad> Configuracion_MetodoCalculoUtilidad();
        DtoLib.ResultadoEntidad<decimal> Configuracion_TasaCambioActual();
        DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta> Configuracion_ForzarRedondeoPrecioVenta();
        DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio> Configuracion_PreferenciaRegistroPrecio();
        DtoLib.ResultadoEntidad<int > Configuracion_CostoEdadProducto();

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCompras
{
    
    public interface IConfiguracion
    {

        DtoLib.ResultadoEntidad<DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaBusquedaProveedor> Configuracion_PreferenciaBusquedaProveedor();
        DtoLib.ResultadoEntidad<DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaBusquedaProducto> Configuracion_PreferenciaBusquedaProducto();
        DtoLib.ResultadoEntidad<decimal> Configuracion_TasaCambioActual();

    }

}
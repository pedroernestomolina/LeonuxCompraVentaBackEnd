using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibSistema
{
    
    public interface IConfiguracion
    {

        DtoLib.ResultadoEntidad<DtoLibSistema.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta> Configuracion_ForzarRedondeoPrecioVenta();
        DtoLib.ResultadoEntidad<DtoLibSistema.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio> Configuracion_PreferenciaRegistroPrecio();

        DtoLib.ResultadoEntidad<string> Configuracion_TasaCambioActual();
        DtoLib.ResultadoEntidad<string> Configuracion_TasaRecepcionPos();

        DtoLib.Resultado Configuracion_Actualizar_TasaRecepcionPos(DtoLibSistema.Configuracion.ActualizarTasaRecepcionPos.Ficha ficha);
        DtoLib.ResultadoLista<DtoLibSistema.Configuracion.ActualizarTasaDivisa.CapturarData.Ficha> Configuracion_Actualizar_TasaDivisa_CapturarData();
        DtoLib.Resultado Configuracion_Actualizar_TasaDivisa_ActualizarData(DtoLibSistema.Configuracion.ActualizarTasaDivisa.ActualizarData.Ficha ficha);

    }

}
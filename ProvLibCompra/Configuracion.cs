using LibEntityCompra;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibCompra
{

    public partial class Provider: ILibCompras.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaBusquedaProveedor> Configuracion_PreferenciaBusquedaProveedor()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaBusquedaProveedor>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL02");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var modo = DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaBusquedaProveedor.SinDefinir;
                    switch (ent.usuario.Trim().ToUpper())
                    {
                        case "CODIGO":
                            modo = DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaBusquedaProveedor.PorCodigo ;
                            break;
                        case "NOMBRE":
                            modo = DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaBusquedaProveedor.PorNombre ;
                            break;
                        case "CI/RIF":
                            modo = DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaBusquedaProveedor.Rif ;
                            break;
                    }

                    result.Entidad = modo;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<string> Configuracion_TasaCambioActual()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL12");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = ent.usuario;
                    //var m1 = 0.0m;
                    //var cnf = ent.usuario;
                    //if (cnf.Trim() != "")
                    //{
                    //    var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
                    //    //var culture = CultureInfo.CreateSpecificCulture("es-ES");
                    //    var culture = CultureInfo.CreateSpecificCulture("en-EN");
                    //    Decimal.TryParse(cnf, style, culture, out m1);
                    //}
                    //result.Entidad = m1;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaBusquedaProducto> Configuracion_PreferenciaBusquedaProducto()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaBusquedaProducto>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL03");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var modo = DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaBusquedaProducto.SinDefinir;
                    switch (ent.usuario.Trim().ToUpper())
                    {
                        case "CODIGO":
                            modo = DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaBusquedaProducto.PorCodigo;
                            break;
                        case "NOMBRE":
                            modo = DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaBusquedaProducto.PorNombre;
                            break;
                        case "REFERENCIA":
                            modo = DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaBusquedaProducto.Referencia;
                            break;
                    }

                    result.Entidad = modo;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibCompra.Configuracion.Enumerados.EnumMetodoCalculoUtilidad> Configuracion_MetodoCalculoUtilidad()
        {
            var result = new DtoLib.ResultadoEntidad< DtoLibCompra.Configuracion.Enumerados.EnumMetodoCalculoUtilidad>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL13");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var modo = DtoLibCompra.Configuracion.Enumerados.EnumMetodoCalculoUtilidad.SinDefinir;
                    switch (ent.usuario.Trim().ToUpper())
                    {
                        case "LINEAL":
                            modo = DtoLibCompra.Configuracion.Enumerados.EnumMetodoCalculoUtilidad.Lineal;
                            break;
                        case "FINANCIERO":
                            modo = DtoLibCompra.Configuracion.Enumerados.EnumMetodoCalculoUtilidad.Financiero;
                            break;
                    }

                    result.Entidad = modo;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibCompra.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta> Configuracion_ForzarRedondeoPrecioVenta()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL46");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var modo = DtoLibCompra.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta.SinDefinir;
                    switch (ent.usuario.Trim().ToUpper())
                    {
                        case "SIN REDONDEO":
                            modo = DtoLibCompra.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta.SinRedeondeo;
                            break;
                        case "UNIDAD":
                            modo = DtoLibCompra.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta.Unidad;
                            break;
                        case "DECENA":
                            modo = DtoLibCompra.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta.Decena;
                            break;
                    }

                    result.Entidad = modo;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio> Configuracion_PreferenciaRegistroPrecio()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL41");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var modo = DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio.SinDefinir;
                    switch (ent.usuario.Trim().ToUpper())
                    {
                        case "PRECIO NETO":
                            modo = DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio.Neto;
                            break;
                        case "PRECIO+IVA":
                            modo = DtoLibCompra.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio.Full;
                            break;
                    }

                    result.Entidad = modo;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

    }

}
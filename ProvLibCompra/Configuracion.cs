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

        public DtoLib.ResultadoEntidad<decimal> Configuracion_TasaCambioActual()
        {
            var result = new DtoLib.ResultadoEntidad<decimal>();

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

                    var m1 = 0.0m;
                    var cnf = ent.usuario;
                    if (cnf.Trim() != "")
                    {
                        var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
                        //var culture = CultureInfo.CreateSpecificCulture("es-ES");
                        var culture = CultureInfo.CreateSpecificCulture("en-EN");
                        Decimal.TryParse(cnf, style, culture, out m1);
                    }
                    result.Entidad = m1;
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

    }

}
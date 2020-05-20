using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibVenta
{

    public partial class Provider: ILibVentas.IProvider 
    {

        public DtoLib.ResultadoEntidad<decimal> FactorCambioDivisa()
        {
            var result = new DtoLib.ResultadoEntidad<decimal>();

            try
            {

                using (var ctx = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    decimal m1 = 0;
                    var cnf = ctx.Database.SqlQuery<string>("select usuario from sistema_configuracion where codigo='GLOBAL12'").FirstOrDefault();
                    if (cnf == null)
                    {
                        result.Entidad = 0.0m;
                        return result;
                    }

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

        public DtoLib.ResultadoEntidad<decimal> FactorCambioDivisaParaRecibir()
        {
            var result = new DtoLib.ResultadoEntidad<decimal>();

            try
            {

                using (var ctx = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    decimal m1 = 0;
                    var cnf = ctx.Database.SqlQuery<string>("select usuario from sistema_configuracion where codigo='GLOBAL48'").FirstOrDefault();
                    if (cnf == null) 
                    {
                        result.Entidad = 0.0m;
                        return result;
                    }

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

        public DtoLib.ResultadoEntidad<DtoLibVenta.Sistema.Enumerados.enumPreferenciaBusquedaProducto> PreferenciaBusquedaProducto()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibVenta.Sistema.Enumerados.enumPreferenciaBusquedaProducto>();

            try
            {

                using (var ctx = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var prf = DtoLibVenta.Sistema.Enumerados.enumPreferenciaBusquedaProducto.SinDefinir;

                    var cnf = ctx.Database.SqlQuery<string>("select usuario from sistema_configuracion where codigo='GLOBAL03'").FirstOrDefault();
                    if (cnf == null)
                    {
                        result.Entidad = prf;
                        return result;
                    }

                    if (cnf.Trim() != "")
                    {
                        switch (cnf.Trim().ToUpper())
                        {
                            case "CÓDIGO":
                                prf = DtoLibVenta.Sistema.Enumerados.enumPreferenciaBusquedaProducto.Codigo;
                                break;
                            case "NOMBRE":
                                prf = DtoLibVenta.Sistema.Enumerados.enumPreferenciaBusquedaProducto.Nombre;
                                break;
                            case "REFERENCIA":
                                prf = DtoLibVenta.Sistema.Enumerados.enumPreferenciaBusquedaProducto.Referencia;
                                break;
                        }
                    }

                    result.Entidad = prf;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibVenta.Sistema.Enumerados.enumPreferenciaBusquedaCliente> PreferenciaBusquedaCliente()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibVenta.Sistema.Enumerados.enumPreferenciaBusquedaCliente>();

            try
            {

                using (var ctx = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var prf = DtoLibVenta.Sistema.Enumerados.enumPreferenciaBusquedaCliente.SinDefinir;

                    var cnf = ctx.Database.SqlQuery<string>("select usuario from sistema_configuracion where codigo='GLOBAL01'").FirstOrDefault();
                    if (cnf == null)
                    {
                        result.Entidad = prf;
                        return result;
                    }

                    if (cnf.Trim() != "")
                    {
                        switch (cnf.Trim().ToUpper())
                        {
                            case "CÓDIGO":
                                prf = DtoLibVenta.Sistema.Enumerados.enumPreferenciaBusquedaCliente.Codigo;
                                break;
                            case "NOMBRE":
                                prf = DtoLibVenta.Sistema.Enumerados.enumPreferenciaBusquedaCliente.Nombre;
                                break;
                            case "CI/RIF":
                                prf = DtoLibVenta.Sistema.Enumerados.enumPreferenciaBusquedaCliente.CiRif;
                                break;
                        }
                    }

                    result.Entidad = prf;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibVenta.Sistema.Enumerados.enumMetodoCalculoUtilidad> MetodoCalculoUtilidad()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibVenta.Sistema.Enumerados.enumMetodoCalculoUtilidad>();

            try
            {

                using (var ctx = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var prf = DtoLibVenta.Sistema.Enumerados.enumMetodoCalculoUtilidad.SinDefinir;

                    var cnf = ctx.Database.SqlQuery<string>("select usuario from sistema_configuracion where codigo='GLOBAL13'").FirstOrDefault();
                    if (cnf == null)
                    {
                        result.Entidad = prf;
                        return result;
                    }

                    if (cnf.Trim() != "")
                    {
                        switch (cnf.Trim().ToUpper())
                        {
                            case "LINEAL":
                                prf = DtoLibVenta.Sistema.Enumerados.enumMetodoCalculoUtilidad.Lineal ;
                                break;
                            case "FINANCIERO":
                                prf = DtoLibVenta.Sistema.Enumerados.enumMetodoCalculoUtilidad.Financiero;
                                break;
                        }
                    }

                    result.Entidad = prf;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<bool> HabilitarRupturaPorExistencia()
        {
            var result = new DtoLib.ResultadoEntidad<bool>();

            try
            {
                var rt = false;
                using (var ctx = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var cnf = ctx.Database.SqlQuery<string>("select usuario from sistema_configuracion where codigo='GLOBAL14'").FirstOrDefault();
                    if (cnf == null)
                    {
                        result.Entidad = true;
                        return result;
                    }

                    if (cnf.Trim() != "")
                    {
                        switch (cnf.Trim().ToUpper())
                        {
                            case "SI":
                                rt = true;
                                break;
                            case "NO":
                                rt = false;
                                break;
                        }
                    }

                    result.Entidad = rt;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<bool> HabilitarAlertaPorExistenciaEnNegativa()
        {
            var result = new DtoLib.ResultadoEntidad<bool>();

            try
            {
                var rt = false;
                using (var ctx = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var cnf = ctx.Database.SqlQuery<string>("select usuario from sistema_configuracion where codigo='GLOBAL31'").FirstOrDefault();
                    if (cnf == null)
                    {
                        result.Entidad = true;
                        return result;
                    }

                    if (cnf.Trim() != "")
                    {
                        switch (cnf.Trim().ToUpper())
                        {
                            case "SI":
                                rt = true;
                                break;
                            case "NO":
                                rt = false;
                                break;
                        }
                    }

                    result.Entidad = rt;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<string> Clave_NivelMinimo()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var ctx = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var cnf = ctx.Database.SqlQuery<string>("select usuario from sistema_configuracion where codigo='GLOBAL19'").FirstOrDefault();
                    if (cnf == null)
                    {
                        result.Entidad = "";
                        return result;
                    }

                    if (cnf.Trim() != "")
                    {
                        result.Entidad = cnf.Trim();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<string> Clave_NivelMedio()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var ctx = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var cnf = ctx.Database.SqlQuery<string>("select usuario from sistema_configuracion where codigo='GLOBAL18'").FirstOrDefault();
                    if (cnf == null)
                    {
                        result.Entidad = "";
                        return result;
                    }

                    if (cnf.Trim() != "")
                    {
                        result.Entidad = cnf.Trim();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<string> Clave_NivelMaximo()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var ctx = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var cnf = ctx.Database.SqlQuery<string>("select usuario from sistema_configuracion where codigo='GLOBAL17'").FirstOrDefault();
                    if (cnf == null)
                    {
                        result.Entidad = "";
                        return result;
                    }

                    if (cnf.Trim() != "")
                    {
                        result.Entidad = cnf.Trim();
                    }
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
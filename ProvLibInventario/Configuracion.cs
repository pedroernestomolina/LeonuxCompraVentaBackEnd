﻿using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda> Configuracion_PreferenciaBusqueda()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f=>f.codigo=="GLOBAL03");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var modo=DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda.SinDefinir;
                    switch (ent.usuario.Trim().ToUpper()) 
                    {
                        case "CODIGO":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda.PorCodigo;
                            break;
                        case "NOMBRE":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda.PorNombre;
                            break;
                        case "REFERENCIA":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda.PorReferencia;
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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumMetodoCalculoUtilidad> Configuracion_MetodoCalculoUtilidad()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumMetodoCalculoUtilidad>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL13");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var modo = DtoLibInventario.Configuracion.Enumerados.EnumMetodoCalculoUtilidad.SinDefinir;
                    switch (ent.usuario.Trim().ToUpper())
                    {
                        case "LINEAL":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumMetodoCalculoUtilidad.Lineal;
                            break;
                        case "FINANCIERO":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumMetodoCalculoUtilidad.Financiero;
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
                using (var cnn = new invEntities(_cnInv.ConnectionString))
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

    }

}
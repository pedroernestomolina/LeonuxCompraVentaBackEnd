using LibEntitySistema;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibSistema
{
    
    public partial class Provider : ILibSistema.IProvider
    {

        public DtoLib.ResultadoEntidad<string> Configuracion_TasaCambioActual()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL12");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = ent.usuario;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<string> Configuracion_TasaRecepcionPos()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL48");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = ent.usuario;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        //public DtoLib.ResultadoEntidad<decimal> Configuracion_TasaCambioActual()
        //{
        //    var result = new DtoLib.ResultadoEntidad<decimal>();

        //    try
        //    {
        //        using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
        //        {
        //            var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL12");
        //            if (ent == null)
        //            {
        //                result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
        //                result.Result = DtoLib.Enumerados.EnumResult.isError;
        //                return result;
        //            }

        //            var m1 = 0.0m;
        //            var cnf = ent.usuario;
        //            if (cnf.Trim() != "")
        //            {
        //                var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
        //                //var culture = CultureInfo.CreateSpecificCulture("es-ES");
        //                var culture = CultureInfo.CreateSpecificCulture("en-EN");
        //                Decimal.TryParse(cnf, style, culture, out m1);
        //            }
        //            result.Entidad = m1;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        result.Mensaje = e.Message;
        //        result.Result = DtoLib.Enumerados.EnumResult.isError;
        //    }

        //    return result;
        //}

        //public DtoLib.ResultadoEntidad<decimal> Configuracion_TasaRecepcionPos()
        //{
        //    var result = new DtoLib.ResultadoEntidad<decimal>();

        //    try
        //    {
        //        using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
        //        {
        //            var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL48");
        //            if (ent == null)
        //            {
        //                result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
        //                result.Result = DtoLib.Enumerados.EnumResult.isError;
        //                return result;
        //            }

        //            var m1 = 0.0m;
        //            var cnf = ent.usuario;
        //            if (cnf.Trim() != "")
        //            {
        //                var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
        //                //var culture = CultureInfo.CreateSpecificCulture("es-ES");
        //                var culture = CultureInfo.CreateSpecificCulture("en-EN");
        //                Decimal.TryParse(cnf, style, culture, out m1);
        //            }
        //            result.Entidad = m1;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        result.Mensaje = e.Message;
        //        result.Result = DtoLib.Enumerados.EnumResult.isError;
        //    }

        //    return result;
        //}

        public DtoLib.Resultado Configuracion_Actualizar_TasaRecepcionPos(DtoLibSistema.Configuracion.ActualizarTasaRecepcionPos.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL48");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    ent.usuario = ficha.ValorNuevo.ToString();
                    cnn.SaveChanges();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoLista<DtoLibSistema.Configuracion.ActualizarTasaDivisa.CapturarData.Ficha> Configuracion_Actualizar_TasaDivisa_CapturarData()
        {
            var result = new DtoLib.ResultadoLista<DtoLibSistema.Configuracion.ActualizarTasaDivisa.CapturarData.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var sql = "SELECT auto , estatus_Divisa, divisa, costo, contenido_compras, " +
                        "pdf_1, pdf_2, pdf_3, pdf_4, pdf_pto, tasa, precio_1, precio_2, precio_3, precio_4, precio_pto  FROM productos " +
                        "where estatus='Activo'";

                    var list = cnn.Database.SqlQuery<DtoLibSistema.Configuracion.ActualizarTasaDivisa.CapturarData.Ficha>(sql).ToList();
                    result.Lista = list;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }


        public DtoLib.ResultadoEntidad<DtoLibSistema.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta> Configuracion_ForzarRedondeoPrecioVenta()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL46");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var modo = DtoLibSistema.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta.SinDefinir;
                    switch (ent.usuario.Trim().ToUpper())
                    {
                        case "SIN REDONDEO":
                            modo = DtoLibSistema.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta.SinRedeondeo;
                            break;
                        case "UNIDAD":
                            modo = DtoLibSistema.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta.Unidad;
                            break;
                        case "DECENA":
                            modo = DtoLibSistema.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta.Decena;
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

        public DtoLib.ResultadoEntidad<DtoLibSistema.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio> Configuracion_PreferenciaRegistroPrecio()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL41");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var modo = DtoLibSistema.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio.SinDefinir;
                    switch (ent.usuario.Trim().ToUpper())
                    {
                        case "PRECIO NETO":
                            modo = DtoLibSistema.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio.Neto;
                            break;
                        case "PRECIO+IVA":
                            modo = DtoLibSistema.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio.Full;
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

        public DtoLib.Resultado Configuracion_Actualizar_TasaDivisa_ActualizarData(DtoLibSistema.Configuracion.ActualizarTasaDivisa.ActualizarData.Ficha ficha)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = cnn.Database.BeginTransaction())
                    {

                        try
                        {
                            var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                            var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL12");
                            if (ent == null)
                            {
                                rt.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                                rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                return rt;
                            }
                            ent.usuario = ficha.ValorDivisa.ToString();
                            cnn.SaveChanges();

                            foreach (var rg in ficha.productosCostoSinDivisa)
                            {
                                var entPrd = cnn.productos.Find(rg.autoPrd);
                                if (entPrd == null)
                                {
                                    rt.Mensaje = "[ ID ] Producto, No Encontrado";
                                    rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return rt;
                                }
                                entPrd.divisa = rg.costoDivisa;
                                entPrd.pdf_1 = rg.precioMonedaEnDivisaFull_1;
                                entPrd.pdf_2 = rg.precioMonedaEnDivisaFull_2;
                                entPrd.pdf_3 = rg.precioMonedaEnDivisaFull_3;
                                entPrd.pdf_4 = rg.precioMonedaEnDivisaFull_4;
                                entPrd.pdf_pto = rg.precioMonedaEnDivisaFull_5;
                                cnn.SaveChanges();
                            }

                            foreach (var rg in ficha.productosCostoPrecioDivisa)
                            {
                                var entPrd = cnn.productos.Find(rg.autoPrd);
                                if (entPrd == null)
                                {
                                    rt.Mensaje = "[ ID ] Producto, No Encontrado";
                                    rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return rt;
                                }
                                entPrd.costo_proveedor = rg.costoProveedor;
                                entPrd.costo_proveedor_und = rg.costoProveedorUnd;
                                entPrd.costo_importacion = rg.costoImportacion;
                                entPrd.costo_importacion_und = rg.costoImportacionUnd;
                                entPrd.costo_varios = rg.costoVario;
                                entPrd.costo_varios_und = rg.costoVarioUnd;
                                entPrd.costo = rg.costo;
                                entPrd.costo_und = rg.costoUnd;
                                entPrd.fecha_ult_costo = fechaSistema.Date;
                                entPrd.fecha_cambio = fechaSistema.Date;

                                entPrd.precio_1 = rg.precio_1;
                                entPrd.precio_2 = rg.precio_2;
                                entPrd.precio_3 = rg.precio_3;
                                entPrd.precio_4 = rg.precio_4;
                                entPrd.precio_pto = rg.precio_5;
                                cnn.SaveChanges();
                            }

                            cnn.Configuration.AutoDetectChangesEnabled = false;
                            var lentHist = new List<productos_costos>();
                            foreach (var rg in ficha.productosCostoPrecioDivisa)
                            {
                                var entHist = new productos_costos()
                                {
                                    auto_producto = rg.autoPrd,
                                    costo = rg.costo,
                                    costo_divisa = rg.costoDivisa,
                                    divisa = ficha.ValorDivisa,
                                    documento = rg.documento,
                                    estacion = ficha.EstacionEquipo,
                                    fecha = fechaSistema.Date,
                                    hora = fechaSistema.ToShortTimeString(),
                                    nota = rg.nota,
                                    serie = rg.serie,
                                    usuario = ficha.nombreUsuario,
                                };
                                lentHist.Add(entHist);
                            }
                            cnn.productos_costos.AddRange(lentHist);
                            cnn.SaveChanges();

                            var lentHist_2 = new List<productos_precios>();
                            foreach (var rg in ficha.productosPrecioHistorico)
                            {
                                var entHist = new productos_precios()
                                {
                                    auto_producto = rg.autoPrd,
                                    estacion = ficha.EstacionEquipo,
                                    fecha = fechaSistema.Date,
                                    hora = fechaSistema.ToShortTimeString(),
                                    usuario = ficha.nombreUsuario,
                                    nota = rg.nota,
                                    precio = rg.precio,
                                    precio_id = rg.idPrecio,
                                };
                                lentHist_2.Add(entHist);
                            }
                            cnn.productos_precios.AddRange(lentHist_2);
                            cnn.SaveChanges();

                            ts.Commit();
                        }
                        catch (DbEntityValidationException e)
                        {
                            var msg = "";
                            foreach (var eve in e.EntityValidationErrors)
                            {
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    msg += ve.ErrorMessage;
                                }
                            }
                            rt.Mensaje = msg;
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        }
                        catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                        {
                            var msg = "";
                            foreach (var eve in e.Entries)
                            {
                                //msg += eve.m;
                                foreach (var ve in eve.CurrentValues.PropertyNames)
                                {
                                    msg += ve.ToString();
                                }
                            }
                            rt.Mensaje = msg;
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        }
                        finally 
                        {
                            cnn.Configuration.AutoDetectChangesEnabled = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

    }

}
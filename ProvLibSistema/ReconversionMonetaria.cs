using LibEntitySistema;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibSistema
{

    public partial class Provider : ILibSistema.IProvider
    {

        public DtoLib.Resultado ReconversionMonetaria_Actualizar(DtoLibSistema.ReconversionMonetaria.Actualizar.Ficha ficha)
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
                                rt.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO"+Environment.NewLine+"TASA DIVISA";
                                rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                return rt;
                            }
                            ent.usuario = ficha.tasaDivisa.ToString();
                            cnn.SaveChanges();

                            var entTasaPos = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL48");
                            if (entTasaPos == null)
                            {
                                rt.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO"+Environment.NewLine+"TASA DIVISA POS";
                                rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                return rt;
                            }
                            entTasaPos.usuario = ficha.tasaDivisaPos.ToString();
                            cnn.SaveChanges();

                            var entRec = new reconversion_monetaria()
                            {
                                fecha = fechaSistema.Date,
                                hora = fechaSistema.ToShortTimeString(),
                                factor_reconversion = ficha.factorReconverion,
                                factor_divisa = ficha.tasaDivisa,
                                usuario = ficha.usuario,
                                codigo_usuario = ficha.codUsuario,
                                idUsuario = ficha.idUsuario,
                                equipo_estacion = ficha.equipoEstacion,
                                items_afectados = ficha.ItemsAfectados,
                                proveedores_afectados = ficha.ProvAfectados,
                                saldos_por_pagar_afectados = ficha.SaldoPorPagarAfectados,
                            };
                            cnn.reconversion_monetaria.Add(entRec);
                            cnn.SaveChanges();

                            foreach (var rg in ficha.Producto)
                            {
                                var entPrd = cnn.productos.Find(rg.autoId);
                                if (entPrd == null)
                                {
                                    rt.Mensaje = "[ ID ] Producto No Encontrado: "+rg.autoId+Environment.NewLine+rg.nombre;
                                    rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return rt;
                                }
                                entPrd.costo_proveedor = rg.costoPrv;
                                entPrd.costo_proveedor_und = rg.costoPrvUnd;
                                entPrd.costo_promedio = rg.costoProm;
                                entPrd.costo_promedio_und= rg.costoPromUnd;
                                entPrd.costo = rg.costo;
                                entPrd.costo_und = rg.costoUnd;
                                entPrd.fecha_cambio = fechaSistema.Date;
                                entPrd.precio_1 = rg.precio1;
                                entPrd.precio_2 = rg.precio2;
                                entPrd.precio_3 = rg.precio3;
                                entPrd.precio_4 = rg.precio4;
                                entPrd.precio_pto = rg.precio5;
                                cnn.SaveChanges();
                            }

                            foreach (var rg in ficha.Proveedor)
                            {
                                var entPrv = cnn.proveedores.Find(rg.autoId);
                                if (entPrv == null)
                                {
                                    rt.Mensaje = "[ ID ] Proveedor No Encontrado: " + rg.autoId + Environment.NewLine + rg.nombre;
                                    rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return rt;
                                }
                                entPrv.anticipos = rg.anticipos;
                                entPrv.debitos = rg.debitos;
                                entPrv.creditos = rg.creditos;
                                entPrv.saldo = rg.saldo;
                                entPrv.disponible= rg.disponible;
                                cnn.SaveChanges();
                            }

                            foreach (var rg in ficha.SaldoPorPagar)
                            {
                                var entCxP = cnn.cxp.Find(rg.autoDoc);
                                if (entCxP == null)
                                {
                                    rt.Mensaje = "[ ID ] Saldo Por Pagar No Encontrado: " + rg.autoDoc + Environment.NewLine + rg.docNumero;
                                    rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return rt;
                                }
                                entCxP.importe = rg.importe;
                                entCxP.acumulado = rg.acumulado;
                                entCxP.resta = rg.resta;
                                cnn.SaveChanges();
                            }

                            cnn.Configuration.AutoDetectChangesEnabled = false;
                            var lentHist = new List<productos_costos>();
                            foreach (var rg in ficha.HistoricoCosto)
                            {
                                var entHist = new productos_costos()
                                {
                                    auto_producto = rg.autoPrd,
                                    costo = rg.costo,
                                    costo_divisa = rg.costoDivisa,
                                    divisa = rg.tasaDivisa,
                                    documento = rg.documento,
                                    estacion = rg.estacionEquipo,
                                    fecha = fechaSistema.Date,
                                    hora = fechaSistema.ToShortTimeString(),
                                    nota = rg.nota,
                                    serie = rg.serie,
                                    usuario = rg.usuario ,
                                };
                                lentHist.Add(entHist);
                            }
                            cnn.productos_costos.AddRange(lentHist);
                            cnn.SaveChanges();

                            var lentHist_2 = new List<productos_precios>();
                            foreach (var rg in ficha.HistoricoPrecio)
                            {
                                var entHist = new productos_precios()
                                {
                                    auto_producto = rg.autoPrd,
                                    estacion = rg.estacionEquipo,
                                    fecha = fechaSistema.Date,
                                    hora = fechaSistema.ToShortTimeString(),
                                    usuario = rg.usuario,
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

        public DtoLib.ResultadoEntidad<DtoLibSistema.ReconversionMonetaria.Data.Ficha> ReconversionMonetaria_GetData()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.ReconversionMonetaria.Data.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var sql_1 = @"SELECT auto as autoId, nombre, 
                                    divisa as costoDivisa,
                                    costo, costo_und as costoUnd, 
                                    costo_promedio as costoProm, costo_promedio_und as costoPromUnd, 
                                    costo_proveedor as costoProv, costo_proveedor_und as costoProvUnd,
                                    precio_1 as precio1, precio_2 as precio2, precio_3 as precio3, 
                                    precio_4 as precio4, precio_pto as precio5
                                    FROM productos
                                    where estatus='Activo' and categoria='Producto Terminado'";
                    var sql_2 = @"SELECT auto as autoId, razon_social as nombre, anticipos, debitos, 
                                    creditos, saldo, disponible 
                                    FROM proveedores";
                    var sql_3 = @"SELECT auto as autoDoc, documento as docNumero, 
                                    importe, acumulado, resta
                                    FROM cxp
                                    where estatus_anulado='0' and estatus_cancelado='0'";

                    var list_1 = cnn.Database.SqlQuery<DtoLibSistema.ReconversionMonetaria.Data.ItemPrd>(sql_1).ToList();
                    var list_2 = cnn.Database.SqlQuery<DtoLibSistema.ReconversionMonetaria.Data.ItemProv>(sql_2).ToList();
                    var list_3 = cnn.Database.SqlQuery<DtoLibSistema.ReconversionMonetaria.Data.ItemSaldoPorPagar>(sql_3).ToList();
                    var ent = new DtoLibSistema.ReconversionMonetaria.Data.Ficha()
                    {
                        Producto = list_1,
                        Proveedor = list_2,
                        SaldoPorPagar = list_3,
                    };
                    result.Entidad = ent;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<int> ReconversionMonetaria_GetCount()
        {
            var result = new DtoLib.ResultadoEntidad<int>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var cnt = cnn.reconversion_monetaria.Count();
                    result.Entidad = cnt;
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
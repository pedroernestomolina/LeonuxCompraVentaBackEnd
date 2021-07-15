using LibEntityCajaBanco;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibCajaBanco
{

    public partial class Provider : ILibCajaBanco.IProvider
    {

        public class pag
        {
            public decimal monto { get; set; }
        }
        public class enc
        {
            public string auto { get; set; }
            public string documento { get; set; }
            public DateTime fecha { get; set; }
            public string nombreRazonSocial { get; set; }
            public string ciRif { get; set; }
            public decimal montoRecibido { get; set; }
            public string codigoMedio { get; set; }
            public string nombreMedio { get; set; }
        }

        public DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.ArqueoCajaPos.Ficha> CajaBanco_ArqueoCajaPos(DtoLibCajaBanco.Reporte.Movimiento.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.ArqueoCajaPos.Ficha>();

            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {
                    var list = new List<DtoLibCajaBanco.Reporte.Movimiento.ArqueoCajaPos.Ficha>();
                    var mov = cnn.pos_arqueo.ToList();
                    if (filtro.desdeFecha.HasValue)
                    {
                        var desde = filtro.desdeFecha.Value;
                        mov = mov.Where(f => f.fecha >= desde).ToList();
                    }
                    if (filtro.hastaFecha.HasValue)
                    {
                        var hasta = filtro.hastaFecha.Value;
                        mov = mov.Where(f => f.fecha <= hasta).ToList();
                    }
                    if (filtro.autoSucursal.Trim() != "")
                    {
                        mov = mov.Where(f => f.auto_cierre.Substring(0, 2) == filtro.autoSucursal).ToList();
                    }

                    if (mov != null)
                    {
                        if (mov.Count() > 0)
                        {
                            list = mov.Select(s =>
                            {
                                var lDivisa = cnn.cxc_medio_pago.Where(w => w.cierre == s.auto_cierre &&
                                    w.estatus_anulado == "0" && w.codigo == "02").Select(st => st.lote).ToList();
                                var tcntDivisa = 0.0m;
                                foreach (var dv in lDivisa)
                                {
                                    var cnt = 0.0m;
                                    var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
                                    //var culture = CultureInfo.CreateSpecificCulture("es-ES");
                                    var culture = CultureInfo.CreateSpecificCulture("en-EN");
                                    Decimal.TryParse(dv, style, culture, out cnt);

                                    //var cnt = int.Parse(dv );
                                    tcntDivisa += cnt;
                                };

                                var rt = new DtoLibCajaBanco.Reporte.Movimiento.ArqueoCajaPos.Ficha()
                                {
                                    autoCierre = s.auto_cierre.Substring(4),
                                    sucursal = s.auto_cierre.Substring(0, 2),
                                    equipo = s.auto_cierre.Substring(2, 2),
                                    autoUsuario = s.auto_usuario,
                                    codigoUsuario = s.codigo,
                                    fecha = s.fecha,
                                    hora = s.hora,
                                    nombreUsuario = s.usuario,
                                    diferencia = s.diferencia,
                                    efectivo = s.efectivo,
                                    divisa = s.cheque,
                                    tarjeta = s.debito,
                                    otros = s.otros,
                                    firma = s.firma,
                                    devolucion = s.devolucion,
                                    subtotal = s.subtotal,
                                    total = s.total,
                                    mefectivo = s.mefectivo,
                                    mdivisa = s.mcheque,
                                    mtarjeta = s.mtarjeta,
                                    motros = s.motros,
                                    msubtotal = s.msubtotal,
                                    mtotal = s.mtotal,
                                    cntdivisa = tcntDivisa,
                                };
                                return rt;
                            }).ToList();
                        }
                    }
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

        public void Reporte(DateTime fecha)
        {
            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {
                    var mov = cnn.cxc_recibos.
                        Join(cnn.cxc_medio_pago, v => v.auto, vp => vp.auto_recibo, (v, vp) => new enc
                        {
                            auto = v.auto,
                            documento = v.documento,
                            ciRif = v.ci_rif,
                            nombreRazonSocial = v.cliente,
                            fecha = v.fecha,
                            montoRecibido = vp.monto_recibido,
                            codigoMedio = vp.codigo,
                            nombreMedio = vp.medio,
                        }).
                        Where(w => w.fecha == fecha).
                        ToList();
                }
            }
            catch (Exception e)
            {
            }
        }

        public DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.Inventario.Ficha> Reporte_InventarioResumen(DtoLibCajaBanco.Reporte.Movimiento.Inventario.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.Inventario.Ficha>();

            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {
                    var sql = @"select p.codigo as codigoPrd, p.nombre as nombrePrd, pmed.decimales as decimales, 
                        (select sum(cantidad_und) from productos_kardex where auto_producto=p.auto and auto_deposito=@autoDeposito and signo=1 and estatus_anulado='0' and fecha<=@hasta) as tEntradas,
                        (select sum(cantidad_und) from productos_kardex where auto_producto=p.auto and auto_deposito=@autoDeposito and signo=-1 and estatus_anulado='0' and fecha<=@hasta) as tSalidas,
                        (select sum(cantidad_und) from productos_kardex where auto_producto=p.auto and auto_deposito=@autoDeposito and fecha>=@desde and fecha<=@hasta and signo=-1 and estatus_anulado='0') as salidas, 
                        (select sum(cantidad_und) from productos_kardex where auto_producto=p.auto and auto_deposito=@autoDeposito and fecha>=@desde and fecha<=@hasta and signo=1 and estatus_anulado='0' and modulo='Inventario') as entradas, 
                        (select sum(cantidad_und) from productos_kardex where auto_producto=p.auto and auto_deposito=@autoDeposito and fecha>=@desde and fecha<=@hasta and signo=1 and estatus_anulado='0' and modulo<>'Inventario') as entradasOt 
                        from productos as p 
                        join productos_medida as pmed on pmed.auto=p.auto_empaque_compra
                        join productos_deposito as pd on pd.auto_producto=p.auto and pd.auto_deposito=@autoDeposito";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desdeFecha;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hastaFecha;
                    p3.ParameterName = "@autoDeposito";
                    p3.Value = filtro.autoDeposito;

                    cnn.Database.CommandTimeout = 0;
                    var list = cnn.Database.SqlQuery<DtoLibCajaBanco.Reporte.Movimiento.Inventario.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.ResumenVenta.Ficha> Reporte_VentaResumen(DtoLibCajaBanco.Reporte.Movimiento.ResumenVenta.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.ResumenVenta.Ficha>();

            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {
                    var sql = "SELECT codigo_usuario as usuarioCodigo, usuario as usuarioNombre, fecha, " +
                        "hora, documento, razon_social as clienteNombre,ci_rif as clienteRif, total, " +
                        "signo, tipo, serie, renglones, documento_nombre documentoNombre, " +
                        "condicion_pago as condicionPago, (descuento1+descuento2) as descuento, auto " +
                        "FROM ventas where fecha>=@desde and fecha<=@hasta and codigo_sucursal=@codigoSucursal and estatus_anulado='0'";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desdeFecha;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hastaFecha;
                    p3.ParameterName = "@codigoSucursal";
                    p3.Value = filtro.codigoSucursal;

                    var list = cnn.Database.SqlQuery<DtoLibCajaBanco.Reporte.Movimiento.ResumenVenta.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Habladores.Ficha> Reporte_Habladores(DtoLibCajaBanco.Reporte.Habladores.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Habladores.Ficha>();

            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {
                    var sql = "select distinct p.auto as autoPrd, p.codigo as codigoPrd, p.nombre as nombrePrd, p.precio_1 as pneto_1, " +
                        "precio_2 as pneto_2, precio_3 as pneto_3, precio_4 as pneto_4, precio_pto as pneto_5, " +
                        "p.pdf_1 as pdivisaFull_1, p.pdf_2 as pdivisaFull_2, p.pdf_3 as pdivisaFull_3, " +
                        "p.pdf_4 as pdivisaFull_4, p.pdf_pto as pdivisaFull_5, tasa as tasaIva  " +
                        "from productos_precios as pprc " +
                        "join productos as p on pprc.auto_producto=p.auto " +
                        "where 1=1";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    var list = cnn.Database.SqlQuery<DtoLibCajaBanco.Reporte.Habladores.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.FacturaDetalle.Ficha> Reporte_VentaDetalle(DtoLibCajaBanco.Reporte.Movimiento.FacturaDetalle.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.FacturaDetalle.Ficha>();

            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {
                    var sql = "select v.auto, v.documento, v.fecha, v.usuario as usuarioNombre, v.signo, " +
                        "v.documento_nombre as documentoNombre, v.codigo_usuario as usuarioCodigo, v.total, v.renglones, " +
                        "vd.nombre as nombreProducto, vd.cantidad_und as cantidadUnd, vd.precio_und as precioUnd, " +
                        "vd.total as totalRenglon, v.hora " +
                        "from ventas as v join ventas_detalle as vd on vd.auto_documento=v.auto " +
                        "where v.fecha>=@desde and v.fecha<=@hasta and v.codigo_sucursal=@codigoSucursal and v.estatus_anulado='0'";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desdeFecha;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hastaFecha;
                    p3.ParameterName = "@codigoSucursal";
                    p3.Value = filtro.codigoSucursal;

                    var list = cnn.Database.SqlQuery<DtoLibCajaBanco.Reporte.Movimiento.FacturaDetalle.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.VentasPorProducto.Ficha> Reporte_VentaPorProducto(DtoLibCajaBanco.Reporte.Movimiento.VentasPorProducto.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.VentasPorProducto.Ficha>();

            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {
                    var sql_1 = "SELECT vd.codigo as codigoPrd, vd.nombre as nombrePrd, sum(vd.cantidad_und) as cantidad, " +
                        "sum(vd.total) as totalMonto, v.documento_nombre as nombreDocumento, v.signo, " +
                        "sum(vd.total/v.factor_cambio) as totalMontoDivisa ";
                    var sql_2 = " FROM ventas_detalle as vd " +
                        "join ventas as v on vd.auto_documento=v.auto ";
                    var sql_3 = " where v.fecha>=@desde and v.fecha<=@hasta and v.estatus_anulado='0' ";
                    var sql_4 = " group by vd.auto_producto, vd.codigo, vd.nombre, v.documento_nombre, v.signo ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desdeFecha;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hastaFecha;

                    if (filtro.codigoSucursal != "")
                    {
                        sql_3 += " and v.codigo_sucursal=@codigoSucursal ";
                        p3.ParameterName = "@codigoSucursal";
                        p3.Value = filtro.codigoSucursal;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibCajaBanco.Reporte.Movimiento.VentasPorProducto.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.ResumenVentaSucursal.Ficha> Reporte_ResumenVentaSucursal(DtoLibCajaBanco.Reporte.Movimiento.ResumenVentaSucursal.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.ResumenVentaSucursal.Ficha>();

            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {
                    var sql_1 = "SELECT " +
                        "count(*) as cntMov, " +
                        "sum(v.total) as montoTotal, " +
                        "sum(v.total/v.factor_cambio) as montoDivisa, " +
                        "v.signo, " +
                        "v.documento_nombre as tipoDoc, " +
                        "es.nombre as nombreSuc, " +
                        "es.codigo as codigoSuc ";

                    var sql_2 = " FROM ventas as v " +
                        " join empresa_sucursal as es on es.codigo=v.codigo_sucursal ";

                    var sql_3 = " where fecha>=@desde and fecha<=@hasta and estatus_anulado='0' ";

                    var sql_4 = " group by v.signo, v.documento_nombre, es.codigo, es.nombre ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desdeFecha;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hastaFecha;

                    if (filtro.codigoSucursal != "")
                    {
                        sql_3 += " and v.codigo_sucursal=@codigoSucursal ";
                        p3.ParameterName = "@codigoSucursal";
                        p3.Value = filtro.codigoSucursal;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibCajaBanco.Reporte.Movimiento.ResumenVentaSucursal.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.VentasPorProductoSucursal.Ficha> Reporte_VentaPorProductoSucursal(DtoLibCajaBanco.Reporte.Movimiento.VentasPorProductoSucursal.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.VentasPorProductoSucursal.Ficha>();

            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {
                    var sql_1 = "SELECT " +
                        "es.codigo as codigoSuc, " +
                        "es.nombre as nombreSuc, " +
                        "vd.codigo as codigoPrd, " +
                        "vd.nombre as nombrePrd, " +
                        "sum(vd.cantidad_und) as cantidad, " +
                        "sum(vd.total) as totalMonto, " +
                        "v.documento_nombre as nombreDocumento, " +
                        "v.signo, " +
                        "sum(vd.total/v.factor_cambio) as totalMontoDivisa ";

                    var sql_2 = " FROM ventas_detalle as vd " +
                        "join ventas as v on vd.auto_documento=v.auto " +
                        "join empresa_sucursal as es on es.codigo=v.codigo_sucursal";

                    var sql_3 = " where v.fecha>=@desde and v.fecha<=@hasta and v.estatus_anulado='0' ";

                    var sql_4 = " group by vd.auto_producto, vd.codigo, vd.nombre, v.documento_nombre, v.signo, " +
                        "es.codigo, es.nombre ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desdeFecha;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hastaFecha;

                    if (filtro.codigoSucursal != "")
                    {
                        sql_3 += " and v.codigo_sucursal=@codigoSucursal ";
                        p3.ParameterName = "@codigoSucursal";
                        p3.Value = filtro.codigoSucursal;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibCajaBanco.Reporte.Movimiento.VentasPorProductoSucursal.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        //public DtoLib.ResultadoEntidad<DtoLibCajaBanco.Reporte.Movimiento.CobranzaDiaria.Ficha> Reporte_CobranzaDiara(DtoLibCajaBanco.Reporte.Movimiento.CobranzaDiaria.Filtro filtro)
        //{
        //    var rt = new DtoLib.ResultadoEntidad<DtoLibCajaBanco.Reporte.Movimiento.CobranzaDiaria.Ficha>();

        //    try
        //    {
        //        using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
        //        {
        //            var sql_1 = "SELECT rec.auto, substr(rec.auto,1,2) as codSuc, substr(rec.auto,3,2) as codEstacion, " +
        //                "rec.fecha, rec.hora, rec.documento as reciboNro, rec.importe, rec.cliente, rec.ci_rif as ciRif, rec.cambio, " +
        //                "mp.lote as loteNro, mp.referencia as refNro, mp.monto_recibido as montoRecibido, mp.medio as medioPagoDesc, " +
        //                "mp.codigo as medioPagoCod, " +
        //                "doc.tipo_documento as tipoDocumento, doc.documento as documentoNro, doc.operacion ";

        //            var sql_2 = " FROM cxc_recibos as rec " +
        //                "join cxc_medio_pago as mp on mp.auto_recibo=rec.auto " +
        //                "join cxc_documentos as doc on doc.auto_cxc_recibo=rec.auto ";

        //            var sql_3 = " where rec.estatus_anulado='0' and rec.fecha>=@desde and rec.fecha<=@hasta ";

        //            var sql_4 = "";

        //            var p1 = new MySql.Data.MySqlClient.MySqlParameter();
        //            var p2 = new MySql.Data.MySqlClient.MySqlParameter();
        //            var p3 = new MySql.Data.MySqlClient.MySqlParameter();

        //            p1.ParameterName = "@desde";
        //            p1.Value = filtro.desdeFecha;
        //            p2.ParameterName = "@hasta";
        //            p2.Value = filtro.hastaFecha;

        //            var xsql_1 = "select sum(total) as monto, tipo as tipoDoc, documento_nombre as nombreDoc ";
        //            var xsql_2 = "from ventas ";
        //            var xsql_3 = "where estatus_anulado='0' and fecha>=@desde and fecha<=@hasta ";
        //            var xsql_4 = "group by tipo, documento_nombre ";

        //            var ysql_1 = "select sum(total) as monto ";
        //            var ysql_2 = "from ventas ";
        //            var ysql_3 = "where estatus_anulado='0' and condicion_pago<>'CONTADO' and fecha>=@desde and fecha<=@hasta ";
        //            var ysql_4 = "";

        //            if (filtro.codSucursal != "")
        //            {
        //                sql_3 += " and substr(rec.auto,1,2)=@codSucursal ";
        //                xsql_3 += " and codigo_sucursal=@codSucursal ";
        //                ysql_3 += " and codigo_sucursal=@codSucursal ";
        //                p3.ParameterName = "@codSucursal";
        //                p3.Value = filtro.codSucursal;
        //            }

        //            var sql = sql_1 + sql_2 + sql_3 + sql_4;
        //            var ldata= cnn.Database.SqlQuery<DtoLibCajaBanco.Reporte.Movimiento.CobranzaDiaria.Data>(sql, p1, p2, p3).ToList();

        //            var xsql = xsql_1 + xsql_2 + xsql_3 + xsql_4;
        //            var lmov = cnn.Database.SqlQuery<DtoLibCajaBanco.Reporte.Movimiento.CobranzaDiaria.Movimiento>(xsql, p1, p2, p3).ToList();

        //            var ysql = ysql_1 + ysql_2 + ysql_3 + ysql_4;
        //            var montoCredito= cnn.Database.SqlQuery<decimal?>(ysql, p1, p2, p3).FirstOrDefault();

        //            var ficha= new DtoLibCajaBanco.Reporte.Movimiento.CobranzaDiaria.Ficha()
        //            {
        //                 data=ldata,
        //                 movimiento=lmov,
        //                 montoCredito = montoCredito.HasValue?montoCredito.Value:0.0m,
        //            };

        //            rt.Entidad = ficha;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        rt.Mensaje = e.Message;
        //        rt.Result = DtoLib.Enumerados.EnumResult.isError;
        //    }

        //    return rt;
        //}

        //public DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.ResumenDiarioVentaSucursal.Ficha> Reporte_ResumenDiarioVentaSucursal(DtoLibCajaBanco.Reporte.Movimiento.ResumenDiarioVentaSucursal.Filtro filtro)
        //{
        //    var rt = new DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.ResumenDiarioVentaSucursal.Ficha>();

        //    try
        //    {
        //        using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
        //        {
        //            var sql_1 = "SELECT " +
        //                "count(*) as cntMov, " +
        //                "v.fecha, " +
        //                "sum(v.total) as montoTotal, " +
        //                "sum(v.total/v.factor_cambio) as montoDivisa, " +
        //                "v.signo, " +
        //                "v.documento_nombre as tipoDoc, " +
        //                "es.nombre as nombreSuc, " +
        //                "es.codigo as codigoSuc ";

        //            var sql_2 = " FROM ventas as v " +
        //                " join empresa_sucursal as es on es.codigo=v.codigo_sucursal ";

        //            var sql_3 = " where fecha>=@desde and fecha<=@hasta and estatus_anulado='0' ";

        //            var sql_4 = " group by v.fecha, v.signo, v.documento_nombre, es.codigo, es.nombre ";

        //            var p1 = new MySql.Data.MySqlClient.MySqlParameter();
        //            var p2 = new MySql.Data.MySqlClient.MySqlParameter();
        //            var p3 = new MySql.Data.MySqlClient.MySqlParameter();

        //            p1.ParameterName = "@desde";
        //            p1.Value = filtro.desdeFecha;
        //            p2.ParameterName = "@hasta";
        //            p2.Value = filtro.hastaFecha;

        //            if (filtro.codigoSucursal != "")
        //            {
        //                sql_3 += " and v.codigo_sucursal=@codigoSucursal ";
        //                p3.ParameterName = "@codigoSucursal";
        //                p3.Value = filtro.codigoSucursal;
        //            }

        //            var sql = sql_1 + sql_2 + sql_3 + sql_4;
        //            var list = cnn.Database.SqlQuery<DtoLibCajaBanco.Reporte.Movimiento.ResumenDiarioVentaSucursal.Ficha>(sql, p1, p2, p3).ToList();
        //            rt.Lista = list;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        rt.Mensaje = e.Message;
        //        rt.Result = DtoLib.Enumerados.EnumResult.isError;
        //    }

        //    return rt;
        //}

        public DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.ResumenDiarioVentaSucursal.Ficha> Reporte_ResumenDiarioVentaSucursal(DtoLibCajaBanco.Reporte.Movimiento.ResumenDiarioVentaSucursal.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.ResumenDiarioVentaSucursal.Ficha>();

            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {
                    var sql_1 = "SELECT " +
                        "count(*) as cntMov, " +
                        "v.fecha, " +
                        "sum(v.total*v.signo) as montoTotal, " +
                        "sum((v.total/v.factor_cambio)*v.signo) as montoDivisa, " +
                        "es.nombre as nombreSuc, " +
                        "es.codigo as codigoSuc, " +
                        "v.cierre, substr(v.auto,3,2) as caja, " +
                        "v.signo, v.tipo as tipoDoc, " +
                        "min(lpad(v.hora,5,'0')) as horaI, max(lpad(v.hora,5,'0')) as horaF, " +
                        "min(documento) as docI, max(documento) as docF ";

                    var sql_2 = " FROM ventas as v " +
                        " join empresa_sucursal as es on es.codigo=v.codigo_sucursal ";

                    var sql_3 = " where fecha>=@desde and fecha<=@hasta and estatus_anulado='0' ";

                    var sql_4 = " group by v.fecha, es.codigo, es.nombre, v.cierre, v.signo, v.tipo, caja ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desdeFecha;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hastaFecha;

                    if (filtro.codigoSucursal != "")
                    {
                        sql_3 += " and v.codigo_sucursal=@codigoSucursal ";
                        p3.ParameterName = "@codigoSucursal";
                        p3.Value = filtro.codigoSucursal;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibCajaBanco.Reporte.Movimiento.ResumenDiarioVentaSucursal.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoEntidad<DtoLibCajaBanco.Reporte.Movimiento.CobranzaDiaria.Ficha> Reporte_CobranzaDiara(DtoLibCajaBanco.Reporte.Movimiento.CobranzaDiaria.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibCajaBanco.Reporte.Movimiento.CobranzaDiaria.Ficha>();

            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {
                    var sql_1 = "SELECT rec.auto, substr(rec.auto,1,2) as codSuc, substr(rec.auto,3,2) as codEstacion, " +
                        "rec.fecha, rec.hora, rec.documento as reciboNro, rec.importe, rec.cliente, rec.ci_rif as ciRif, rec.cambio, " +
                        "mp.lote as loteNro, mp.referencia as refNro, mp.monto_recibido as montoRecibido, mp.medio as medioPagoDesc, " +
                        "mp.codigo as medioPagoCod, " +
                        "doc.tipo_documento as tipoDocumento, doc.documento as documentoNro, doc.operacion ";

                    var sql_2 = " FROM cxc_recibos as rec " +
                        "join cxc_medio_pago as mp on mp.auto_recibo=rec.auto " +
                        "join cxc_documentos as doc on doc.auto_cxc_recibo=rec.auto ";

                    var sql_3 = " where rec.estatus_anulado='0' ";

                    var sql_4 = "";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();

                    var xsql_1 = "select sum(total) as monto, tipo as tipoDoc, documento_nombre as nombreDoc ";
                    var xsql_2 = "from ventas ";
                    var xsql_3 = "where estatus_anulado='0' ";
                    var xsql_4 = "group by tipo, documento_nombre ";

                    var ysql_1 = "select sum(total) as monto ";
                    var ysql_2 = "from ventas ";
                    var ysql_3 = "where estatus_anulado='0' and condicion_pago<>'CONTADO' ";
                    var ysql_4 = "";

                    if (filtro.porFecha)
                    {
                        sql_3 += " and rec.fecha>=@desde and rec.fecha<=@hasta ";
                        p1.ParameterName = "@desde";
                        p1.Value = filtro.desdeFecha;
                        p2.ParameterName = "@hasta";
                        p2.Value = filtro.hastaFecha;

                        xsql_3 += " and fecha>=@desde and fecha<=@hasta ";
                        ysql_3 += " and fecha>=@desde and fecha<=@hasta ";
                    }

                    if (filtro.porCierre)
                    {
                        sql_3 += " and substr(rec.cierre,5,6)>=@desdeCierre  and substr(rec.cierre,5,6)<=@hastaCierre ";
                        p4.ParameterName = "@desdeCierre";
                        p4.Value = filtro.desdeCierre;
                        p5.ParameterName = "@hastaCierre";
                        p5.Value = filtro.hastaCierre;

                        xsql_3 += " and substr(cierre,5,6)>=@desdeCierre and substr(cierre,5,6)<=@hastaCierre ";
                        ysql_3 += " and substr(cierre,5,6)>=@desdeCierre and substr(cierre,5,6)<=@hastaCierre ";
                    }

                    if (filtro.codSucursal != "")
                    {
                        sql_3 += " and substr(rec.auto,1,2)=@codSucursal ";
                        xsql_3 += " and codigo_sucursal=@codSucursal ";
                        ysql_3 += " and codigo_sucursal=@codSucursal ";
                        p3.ParameterName = "@codSucursal";
                        p3.Value = filtro.codSucursal;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var ldata = cnn.Database.SqlQuery<DtoLibCajaBanco.Reporte.Movimiento.CobranzaDiaria.Data>(sql, p1, p2, p3, p4, p5).ToList();

                    var xsql = xsql_1 + xsql_2 + xsql_3 + xsql_4;
                    var lmov = cnn.Database.SqlQuery<DtoLibCajaBanco.Reporte.Movimiento.CobranzaDiaria.Movimiento>(xsql, p1, p2, p3, p4, p5).ToList();

                    var ysql = ysql_1 + ysql_2 + ysql_3 + ysql_4;
                    var montoCredito = cnn.Database.SqlQuery<decimal?>(ysql, p1, p2, p3, p4, p5).FirstOrDefault();

                    var ficha = new DtoLibCajaBanco.Reporte.Movimiento.CobranzaDiaria.Ficha()
                    {
                        data = ldata,
                        movimiento = lmov,
                        montoCredito = montoCredito.HasValue ? montoCredito.Value : 0.0m,
                    };

                    rt.Entidad = ficha;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }



        ////



        public DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Analisis.VentaPromedio.Ficha> Reporte_Analisis_VentaPromedio(DtoLibCajaBanco.Reporte.Analisis.VentaPromedio.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Analisis.VentaPromedio.Ficha>();

            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();

                    var xsql_3=@" where v.estatus_anulado='0' and v.fecha>=@desde and v.fecha<=@hasta ";
                    if (filtro.codSucursal != "")
                    {
                        xsql_3 += " and v.codigo_sucursal=@codSucursal ";
                        p3.ParameterName = "@codSucursal";
                        p3.Value = filtro.codSucursal;
                    }

                    var xsql_4=@" group by v.codigo_sucursal, es.nombre, v.mes_relacion, v.ano_relacion, es.autodepositoprincipal ";

                    var xsql_2 =@"SELECT count(*) as cntMov, sum(v.total*v.signo) as venta, sum(v.monto_divisa*v.signo) as ventaDivisa, 
                                                v.codigo_sucursal as codSucursal, 
                                                v.mes_relacion as mes, 
                                                v.ano_relacion as ano, 
                                                es.nombre as sucursal,
                                                (SELECT count(*) FROM productos_deposito as pd    
                                                        join productos as p on pd.auto_producto=p.auto    
                                                        where pd.auto_deposito=es.autodepositoprincipal AND pd.fisica<>0 and p.estatus='Activo' and p.categoria<>'Bien de Servicio'
                                                ) as cntItemStock, 
                                                (SELECT sum(pd.fisica*(p.Divisa/p.contenido_compras))    
                                                        FROM productos_deposito as pd    
                                                        join productos as p on pd.auto_producto=p.auto    
                                                        where pd.auto_deposito=es.autodepositoprincipal AND pd.fisica<>0 and p.estatus='Activo' and p.categoria<>'Bien de Servicio' 
                                                ) as costoStock  
                                        FROM ventas v join empresa_sucursal es on es.codigo=v.codigo_sucursal "+xsql_3+xsql_4;
                    
                    var xsql_1 =@"select v2.*, 
                                        (
	                                        select count(v3.fecha) 	
			                                from 
			                                    (
				                                    select fecha, codigo_sucursal from ventas as v 
					                                where v.estatus_anulado='0' and v.fecha>=@desde and v.fecha<=@hasta
					                                group by v.fecha, v.codigo_sucursal 
			                                    )as v3								
			                                where v3.codigo_sucursal=v2.codSucursal
	                                    ) as dias
                                 from 
                                    ( "+xsql_2+") as v2";

                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;

                    var sql = xsql_1;
                    var ldata = cnn.Database.SqlQuery<DtoLibCajaBanco.Reporte.Analisis.VentaPromedio.Ficha>(sql, p1, p2, p3, p4, p5).ToList();
                    rt.Lista = ldata;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Analisis.VentaProducto.Ficha> Reporte_Analisis_VentaProducto(DtoLibCajaBanco.Reporte.Analisis.VentaProducto.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Analisis.VentaProducto.Ficha>();

            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {

                    var sql_1 = "SELECT sum(vd.cantidad_und*vd.signo) as cnt, vd.auto_producto as autoPrd, p.nombre as nombrePrd, " +
                        "v.mes_relacion as mes, v.ano_relacion as ano";

                    var sql_2 = " FROM ventas_detalle as vd " +
                        "join ventas as v on vd.auto_documento=v.auto " +
                        "join productos as p on vd.auto_producto=p.auto ";

                    var sql_3 = " where v.estatus_anulado='0' and v.fecha>=@desde and v.fecha<=@hasta ";

                    var sql_4 = " group by vd.auto_producto, p.nombre, v.mes_relacion, v.ano_relacion ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;

                    if (filtro.codSucursal != "")
                    {
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var ldata = cnn.Database.SqlQuery<DtoLibCajaBanco.Reporte.Analisis.VentaProducto.Ficha>(sql, p1, p2, p3, p4, p5).ToList();
                    rt.Lista = ldata;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Analisis.VentaDiaria.Ficha> Reporte_Analisis_VentaDiaria(DtoLibCajaBanco.Reporte.Analisis.VentaDiaria.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Analisis.VentaDiaria.Ficha>();

            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                                        
                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;

                    var xsql_1 = @"select v.auto, v.fecha, v.codigo_sucursal as codSucursal, es.nombre as nomSucursal 
                                    from ventas as v 
                                    join empresa_sucursal as es on es.codigo=v.codigo_sucursal
                                    where fecha>=@desde and fecha<=@hasta and estatus_anulado='0'";

                    var sql = xsql_1;
                    var ldata = cnn.Database.SqlQuery<DtoLibCajaBanco.Reporte.Analisis.VentaDiaria.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = ldata;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Analisis.VentaPorCierre.Ficha> Reporte_Analisis_VentaPorCierre(DtoLibCajaBanco.Reporte.Analisis.VentaPorCierre.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Analisis.VentaPorCierre.Ficha>();

            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;

                    var xsql_1 = @"SELECT c.auto_cierre as autoCierre, c.fecha, c.hora, c.cntdoc, c.cntdocfac, c.cntdocncr, 
                                c.efectivo as montoEfectivo, c.cheque as montoDivisa, c.debito as montoDebito,
                                c.otros as montoOtros, c.cnt_divisa as cntDivisa, c.total, 
                                es.nombre as nomSucursal, es.codigo as codSucursal ";
                    var xsql_2 = @" FROM pos_arqueo as c
                                join empresa_sucursal as es on es.codigo=substr(c.auto_cierre,1,2) ";
                    var xsql_3 = @" where c.fecha>=@desde and c.fecha<=@hasta ";

                    var sql = xsql_1+xsql_2+xsql_3;
                    var ldata = cnn.Database.SqlQuery<DtoLibCajaBanco.Reporte.Analisis.VentaPorCierre.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = ldata;
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
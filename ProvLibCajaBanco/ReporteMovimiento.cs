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
            public string codigoMedio{ get; set; }
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
                                    autoCierre= s.auto_cierre.Substring(4),
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
                        Join(cnn.cxc_medio_pago, v => v.auto, vp => vp.auto_recibo, (v,vp) => new enc 
                        { 
                            auto=v.auto, 
                            documento=v.documento, 
                            ciRif=v.ci_rif,
                            nombreRazonSocial=v.cliente,
                            fecha=v.fecha, 
                            montoRecibido=vp.monto_recibido, 
                            codigoMedio=vp.codigo, 
                            nombreMedio=vp.medio,
                        }).
                        Where(w => w.fecha == fecha ).
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
                    var sql = "select p.codigo as codigoPrd, p.nombre as nombrePrd, pmed.decimales as decimales, " +
                        "(select sum(cantidad_und) from productos_kardex where auto_producto=p.auto and auto_deposito=@autoDeposito and signo=1 and estatus_anulado='0' and fecha<=@hasta) as tEntradas, " +
                        "(select sum(cantidad_und) from productos_kardex where auto_producto=p.auto and auto_deposito=@autoDeposito and signo=-1 and estatus_anulado='0' and fecha<=@hasta) as tSalidas, " +
                        "(select sum(cantidad_und) from productos_kardex where auto_producto=p.auto and auto_deposito=@autoDeposito and fecha>=@desde and fecha<=@hasta and signo=-1 and estatus_anulado='0') as salidas, " +
                        "(select sum(cantidad_und) from productos_kardex where auto_producto=p.auto and auto_deposito=@autoDeposito and fecha>=@desde and fecha<=@hasta and signo=1 and estatus_anulado='0' and modulo='Inventario') as entradas, " +
                        "(select sum(cantidad_und) from productos_kardex where auto_producto=p.auto and auto_deposito=@autoDeposito and fecha>=@desde and fecha<=@hasta and signo=1 and estatus_anulado='0' and modulo<>'Inventario') as entradasOt " +
                        "from productos as p " +
                        "join productos_medida as pmed on pmed.auto=p.auto_empaque_compra";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desdeFecha;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hastaFecha;
                    p3.ParameterName = "@autoDeposito";
                    p3.Value = filtro.autoDeposito;

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
                        "v.documento_nombre as documentoNombre, v.codigo_usuario as usuarioCodigo, v.total, v.renglones, "+
                        "vd.nombre as nombreProducto, vd.cantidad_und as cantidadUnd, vd.precio_und as precioUnd, "+
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
                    var sql = "SELECT vd.codigo as codigoPrd, vd.nombre as nombrePrd, sum(vd.cantidad_und) as cantidad, " +
                        "sum(vd.total) as totalMonto, v.documento_nombre as nombreDocumento, v.signo " +
                        "FROM ventas_detalle as vd " +
                        "join ventas as v on vd.auto_documento=v.auto " +
                        "where v.fecha>=@desde and v.fecha<=@hasta and v.codigo_sucursal=@codigoSucursal and v.estatus_anulado='0' " +
                        "group by vd.auto_producto, vd.nombre, v.documento_nombre, vd.signo";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desdeFecha;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hastaFecha;
                    p3.ParameterName = "@codigoSucursal";
                    p3.Value = filtro.codigoSucursal;

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

    }

}
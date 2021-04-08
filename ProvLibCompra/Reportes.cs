using LibEntityCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibCompra
{
    
    public partial class Provider: ILibCompras.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibCompra.Reportes.CompraPorDepartamento.Ficha> Reportes_ComprasPorDepartamento(DtoLibCompra.Reportes.CompraPorDepartamento.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCompra.Reportes.CompraPorDepartamento.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString)) 
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = "SELECT " +
                        "p.auto_departamento as autoDepartamento,  " +
                        "sum(cd.cantidad_und*cd.costo_und) as total, " +
                        "sum((cd.cantidad_und*cd.costo_und)/c.factor_cambio) as totalDivisa, " +
                        "c.signo as signoDoc, " +
                        "c.tipo as tipoDoc, " +
                        "ed.nombre as nombreDepartamento, " +
                        "c.documento_nombre as nombreDoc, " +
                        "c.serie as serieDoc ";

                    var sql_2 = "FROM compras_detalle as cd " +
                        "join compras as c on cd.auto_documento=c.auto " +
                        "join productos as p on cd.auto_producto=p.auto " +
                        "join empresa_departamentos as ed on ed.auto=p.auto_departamento ";

                    var sql_3 = "where c.estatus_anulado='0' ";

                    var sql_4 = "group by ed.nombre, ed.auto, c.tipo, c.signo, c.documento_nombre, c.serie ";

                    sql_3 += " and c.fecha>=@desde ";
                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;

                    sql_3 += " and c.fecha<=@hasta ";
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibCompra.Reportes.CompraPorDepartamento.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibCompra.Reportes.CompraDocumento.Ficha> Reportes_ComprasDocumento(DtoLibCompra.Reportes.CompraDocumento.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCompra.Reportes.CompraDocumento.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = "SELECT " +
                        "fecha, " +
                        "documento, control, " +
                        "serie as serieDoc, " +
                        "estatus_anulado as estatusAnulado, " +
                        "razon_social as provNombre, " +
                        "ci_rif as provCiRif, " +
                        "total, " +
                        "tipo as tipoDoc, " +
                        "monto_divisa as totalDivisa, " +
                        "renglones, " +
                        "factor_cambio as factorDoc, " +
                        "signo as signoDoc, " +
                        "documento_nombre as nombreDoc, " +
                        "(descuento1+descuento2) as montoDscto, " +
                        "cargos as montoCargo ";

                    var sql_2 = "FROM compras as c ";

                    var sql_3 = "where 1=1 ";

                    var sql_4 = "";

                    sql_3 += " and c.fecha>=@desde ";
                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;

                    sql_3 += " and c.fecha<=@hasta ";
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;

                    if (filtro.codSucursal != "") 
                    {
                        sql_3 += " and c.codigo_sucursal=@suc ";
                        p3.ParameterName = "@suc";
                        p3.Value = filtro.codSucursal;
                    }

                    if (filtro.estatus != DtoLibCompra.Reportes.Enumerados.EnumEstatus.SinDefinir)
                    {
                        var xestatus = "0";
                        if (filtro.estatus == DtoLibCompra.Reportes.Enumerados.EnumEstatus.Anulado)
                            xestatus = "1";
                        sql_3 += " and c.estatus_anulado=@estatus ";
                        p4.ParameterName = "@estatus";
                        p4.Value = xestatus;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibCompra.Reportes.CompraDocumento.Ficha>(sql, p1, p2, p3,p4).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibCompra.Reportes.CompraPorProductoDetalle.Ficha> Reportes_CompraPorProductoDetalle(DtoLibCompra.Reportes.CompraPorProductoDetalle.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCompra.Reportes.CompraPorProductoDetalle.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = "SELECT " +
                       "auto_producto as autoPrd, " +
                       "codigo as codigoPrd, " +
                       "nombre as nombrePrd, " +
                       "cantidad_und as cantUnd, " +
                       "costo_und as costoUnd, " +
                       "c.signo as signoDoc, " +
                       "c.documento, " +
                       "c.fecha, " +
                       "c.tipo as tipoDoc, " +
                       "c.serie as serieDoc, " +
                       "c.documento_nombre as nombreDoc, " +
                       "c.factor_cambio as factor ";

                    var sql_2 = "FROM compras_detalle as cd " +
                        "join compras as c on c.auto=cd.auto_documento ";

                    var sql_3 = "where 1=1 and c.estatus_anulado='0' ";

                    var sql_4 = "";

                    sql_3 += " and c.fecha>=@desde ";
                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;

                    sql_3 += " and c.fecha<=@hasta ";
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibCompra.Reportes.CompraPorProductoDetalle.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibCompra.Reportes.CompraPorProducto.Ficha> Reportes_CompraPorProducto(DtoLibCompra.Reportes.CompraPorProducto.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCompra.Reportes.CompraPorProducto.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = "SELECT " +
                        "p.auto as autoPrd, " +
                        "p.codigo as codigoPrd, " +
                        "p.nombre as nombrePrd, " +
                        "sum(cd.cantidad_und) as cantUnd, " +
                        "sum(cd.cantidad_und*cd.costo_und) as total, " +
                        "sum((cd.cantidad_und*cd.costo_und)/c.factor_cambio) as totalDivisa, " +
                        "c.signo as signoDoc, " +
                        "c.tipo as tipoDoc, " +
                        "c.serie as serieDoc, " +
                        "c.documento_nombre as nombreDoc ";

                    var sql_2 = "FROM compras_detalle as cd " +
                        "join compras as c on c.auto=cd.auto_documento " +
                        "join productos as p on p.auto=cd.auto_producto ";

                    var sql_3 = "where 1=1 and c.estatus_anulado='0' ";

                    var sql_4 = "group by p.auto, p.codigo, p.nombre, c.signo, c.tipo, c.serie, c.documento_nombre ";

                    sql_3 += " and c.fecha>=@desde ";
                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;

                    sql_3 += " and c.fecha<=@hasta ";
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibCompra.Reportes.CompraPorProducto.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = lst;
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
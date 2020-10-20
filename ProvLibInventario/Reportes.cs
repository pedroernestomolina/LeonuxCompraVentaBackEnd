using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroProducto.Ficha> Reportes_MaestroProducto(DtoLibInventario.Reportes.MaestroProducto.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroProducto.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = "select p.codigo as codigoPrd , p.nombre as nombrePrd , p.referencia as referenciaPrd, p.modelo as modeloPrd, " +
                        " p.estatus as estatusPrd, p.estatus_divisa as estatusDivisaPrd, p.estatus_cambio as estatusCambioPrd, " +
                        " p.contenido_compras as contenidoPrd, p.origen as origenPrd, p.categoria as categoriaPrd, " +
                        " ed.nombre as departamento, " +
                        " pm.nombre as empaque, " +
                        " etasa.tasa as tasaIva " +
                        " from productos as p " +
                        " join empresa_departamentos as ed on p.auto_departamento=ed.auto " +
                        " join productos_medida as pm on p.auto_empaque_compra=pm.auto " +
                        " join empresa_tasas as etasa on p.auto_tasa=etasa.auto " +
                        " where 1=1 ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p6 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p7 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p8 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p9 = new MySql.Data.MySqlClient.MySqlParameter();
                    var pA = new MySql.Data.MySqlClient.MySqlParameter();
                    var pB = new MySql.Data.MySqlClient.MySqlParameter();
                    var pC = new MySql.Data.MySqlClient.MySqlParameter();
                    var pD = new MySql.Data.MySqlClient.MySqlParameter();


                    if (filtro.autoDepartamento != "")
                    {
                        sql += " and p.auto_departamento=@autoDepartamento ";
                        p1.ParameterName = "@autoDepartamento";
                        p1.Value = filtro.autoDepartamento;
                    }
                    if (filtro.admDivisa != DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.SnDefinir)
                    {
                        var _f = "1";
                        if (filtro.admDivisa == DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.No)
                            _f = "0";
                        sql += " and p.estatus_divisa=@estatusDivisa ";
                        p2.ParameterName = "@estatusDivisa";
                        p2.Value = _f;
                    }
                    if (filtro.autoTasa != "")
                    {
                        sql += " and p.auto_tasa=@autoTasa ";
                        p3.ParameterName = "@autoTasa";
                        p3.Value = filtro.autoTasa;
                    }
                    if (filtro.estatus != DtoLibInventario.Reportes.enumerados.EnumEstatus.SnDefinir)
                    {
                        var _f = "Activo";
                        if (filtro.estatus == DtoLibInventario.Reportes.enumerados.EnumEstatus.Inactivo)
                        {
                            _f = "Inactivo";
                        }
                        sql += " and p.estatus=@estatus ";
                        p4.ParameterName = "@estatus";
                        p4.Value = _f;
                    }
                    if (filtro.categoria != DtoLibInventario.Reportes.enumerados.EnumCategoria.SnDefinir)
                    {
                        var _f = "";
                        switch (filtro.categoria)
                        {
                            case DtoLibInventario.Reportes.enumerados.EnumCategoria.BienServicio:
                                _f = "Bien De Servicio";
                                break;
                            case DtoLibInventario.Reportes.enumerados.EnumCategoria.ProductoTerminado:
                                _f = "Producto Terminado";
                                break;
                            case DtoLibInventario.Reportes.enumerados.EnumCategoria.MateriaPrima:
                                _f = "Materia Prima";
                                break;
                            case DtoLibInventario.Reportes.enumerados.EnumCategoria.SubProducto:
                                _f = "Sub Producto";
                                break;
                            case DtoLibInventario.Reportes.enumerados.EnumCategoria.UsoInterno:
                                _f = "Uso Interno";
                                break;
                        }
                        sql += " and p.categoria=@categoria ";
                        p5.ParameterName = "@categoria";
                        p5.Value = _f;
                    }
                    if (filtro.origen != DtoLibInventario.Reportes.enumerados.EnumOrigen.SnDefinir)
                    {
                        var _f = "Nacional";
                        if (filtro.origen == DtoLibInventario.Reportes.enumerados.EnumOrigen.Importado)
                        {
                            _f = "Importado";
                        }
                        sql += " and p.origen=@origen ";
                        p6.ParameterName = "@origen";
                        p6.Value = _f;
                    }

                    var list = cnn.Database.SqlQuery<DtoLibInventario.Reportes.MaestroProducto.Ficha>(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9, pA, pB, pC, pD).ToList();
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

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroInventario.Ficha> Reportes_MaestroInventario(DtoLibInventario.Reportes.MaestroInventario.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroInventario.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = "select p.codigo as codigoPrd , p.nombre as nombrePrd , p.referencia as referenciaPrd, p.modelo as modeloPrd, " +
                        " p.estatus as estatusPrd, p.estatus_divisa as estatusDivisaPrd, p.estatus_cambio as estatusCambioPrd, " +
                        " p.costo_und as costoUnd, p.divisa as costoDivisaUnd, " +
                        " ed.nombre as departamento, " +
                        " pm.decimales as decimales, " +
                        " (SELECT sum(fisica) from productos_deposito where auto_producto=p.auto) as existencia " +
                        " from productos as p " +
                        " join empresa_departamentos as ed on p.auto_departamento=ed.auto " +
                        " join productos_medida as pm on p.auto_empaque_compra=pm.auto " +
                        " where 1=1 ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();


                    if (filtro.autoDepartamento != "")
                    {
                        sql += " and p.auto_departamento=@autoDepartamento ";
                        p1.ParameterName = "@autoDepartamento";
                        p1.Value = filtro.autoDepartamento;
                    }
                    if (filtro.admDivisa != DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.SnDefinir)
                    {
                        var _f = "1";
                        if (filtro.admDivisa == DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.No)
                            _f = "0";
                        sql += " and p.estatus_divisa=@estatusDivisa ";
                        p2.ParameterName = "@estatusDivisa";
                        p2.Value = _f;
                    }
                    if (filtro.estatus != DtoLibInventario.Reportes.enumerados.EnumEstatus.SnDefinir)
                    {
                        var _f = "Activo";
                        if (filtro.estatus == DtoLibInventario.Reportes.enumerados.EnumEstatus.Inactivo)
                        {
                            _f = "Inactivo";
                        }
                        sql += " and p.estatus=@estatus ";
                        p3.ParameterName = "@estatus";
                        p3.Value = _f;
                    }

                    var list = cnn.Database.SqlQuery<DtoLibInventario.Reportes.MaestroInventario.Ficha>(sql, p1, p2, p3, p4, p5).ToList();
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

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.Top20.Ficha> Reportes_Top20(DtoLibInventario.Reportes.Top20.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.Top20.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = "SELECT abs(sum(pk.cantidad_und*pk.signo)) as cntUnd, p.nombre, pm.decimales as decimales, " +
                        "p.estatus_pesado as estatusPesado, count(*) as cntDoc "+
                        "FROM productos_kardex as pk " +
                        "join productos as p on pk.auto_producto=p.auto " +
                        "join productos_medida as pm on p.auto_empaque_compra=pm.auto " +
                        "where pk.fecha>=@p1 and pk.fecha<=@p2 " +
                        "and pk.modulo=@p3 and pk.estatus_anulado='0' " ;
                    var sql2="group by pk.auto_producto ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@p1";
                    p1.Value = filtro.Desde.Date;

                    p2.ParameterName = "@p2";
                    p2.Value = filtro.Hasta.Date;

                    var modulo="";
                    switch(filtro.Modulo)
                    {
                        case DtoLibInventario.Reportes.enumerados.EnumModulo.Compras:
                            modulo="Compras";
                            break;
                        case DtoLibInventario.Reportes.enumerados.EnumModulo.Ventas:
                            modulo="Ventas";
                            break;
                        case DtoLibInventario.Reportes.enumerados.EnumModulo.Inventario:
                            modulo="Inventario";
                            break;
                    }
                    p3.ParameterName = "@p3";
                    p3.Value = modulo;

                    if (filtro.autoDeposito!="")
                    {
                        sql += "and pk.auto_deposito=@p4 ";
                        p4.ParameterName = "@p4";
                        p4.Value = filtro.autoDeposito;
                    }
                    sql += sql2;

                    var list = cnn.Database.SqlQuery<DtoLibInventario.Reportes.Top20.Ficha>(sql, p1, p2, p3, p4, p5).ToList();
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

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.TopDepartUtilidad.Ficha> Reportes_TopDepartUtilidad(DtoLibInventario.Reportes.TopDepartUtilidad.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.TopDepartUtilidad.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = "SELECT  ed.nombre as Departamento , " +
                        //"abs(sum(((pk.precio_und- pk.costo_und) * (pk.cantidad_und*pk.signo)))) as utilidad, "+
                        "sum( pk.precio_und * (pk.cantidad_und*pk.signo) ) as venta, " +
                        "sum( pk.costo_und * (pk.cantidad_und*pk.signo) ) as costo, " +
                        "count(*) as cntMov, p.auto_departamento " +
                        "FROM productos_kardex as pk " +
                        "join productos as p on pk.auto_producto=p.auto " +
                        "join empresa_departamentos as ed on p.auto_departamento=ed.auto " +
                        "where pk.fecha>=@p1 and pk.fecha<=@p2 " +
                        "and pk.modulo='Ventas' and pk.estatus_anulado='0' " +
                        "and (pk.codigo='01' or pk.codigo='02' or pk.codigo='03') "+
                        "group by p.auto_departamento "; 

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@p1";
                    p1.Value = filtro.Desde;

                    p2.ParameterName = "@p2";
                    p2.Value = filtro.Hasta;

                    var list = cnn.Database.SqlQuery<DtoLibInventario.Reportes.TopDepartUtilidad.Ficha>(sql, p1, p2, p3, p4, p5).ToList();
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

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroExistencia.Ficha> Reportes_MaestroExistencia(DtoLibInventario.Reportes.MaestroExistencia.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroExistencia.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = "SELECT p.auto as autoprd, p.codigo as codigoPrd, p.nombre as nombrePrd, p.estatus as estatusPrd, " +
                        "pdep.fisica as exFisica, edep.auto as autoDep, edep.codigo as codigoDep, edep.nombre as nombreDep, " +
                        "pmed.decimales as decimales "+
                        "FROM productos as p " +
                        "join productos_medida as pmed on p.auto_empaque_compra=pmed.auto "+
                        "left join productos_deposito as pdep on pdep.auto_producto=p.auto " +
                        "left join empresa_depositos as edep on edep.auto=pdep.auto_deposito " +
                        "where p.estatus='Activo' and p.categoria<>'Bien de Servicio' ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();

                    if (filtro.autoDepartamento != "")
                    {
                        sql += " and p.auto_departamento=@autoDepartamento ";
                        p1.ParameterName = "@autoDepartamento";
                        p1.Value = filtro.autoDepartamento;
                    }
                    if (filtro.autoDeposito != "")
                    {
                        sql += " and pdep.auto_deposito=@autoDeposito ";
                        p2.ParameterName = "@autoDeposito";
                        p2.Value = filtro.autoDeposito;
                    }

                    var list = cnn.Database.SqlQuery<DtoLibInventario.Reportes.MaestroExistencia.Ficha>(sql,p1,p2).ToList();
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
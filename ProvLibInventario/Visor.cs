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

        public DtoLib.ResultadoLista<DtoLibInventario.Visor.Existencia.Ficha> Visor_Existencia(DtoLibInventario.Visor.Existencia.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Visor.Existencia.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = "SELECT p.nombre as nombrePrd, p.codigo as codigoPrd, p.auto as autoPrd, " +
                        "case when p.estatus='Activo' then '0' else '1' end as estatusActivo, " +
                        "p.estatus_cambio as estatusSuspendido, " +
                        "pdep.fisica as cntFisica, pdep.nivel_minimo as nivelMinimo, pdep.nivel_optimo as nivelOptimo, " +
                        "case when p.estatus_pesado='0' then 'N' when p.estatus_pesado='1' then 'S' end as esPesado, " +
                        "edep.auto as autoDeposito, edep.nombre as nombreDeposito, edep.codigo as codigoDeposito, " +
                        "edepart.auto as autoDepart, edepart.codigo as codigoDepart, edepart.nombre as nombreDepart, " +
                        "pmed.decimales " +
                        "FROM `productos_deposito` as pdep " +
                        "join empresa_depositos as edep on pdep.auto_deposito=edep.auto " +
                        "join productos as p on pdep.auto_producto=p.auto " +
                        "join empresa_departamentos as edepart on p.auto_departamento=edepart.auto " +
                        "join productos_medida as pmed on p.auto_empaque_compra=pmed.auto " +
                        "WHERE 1 = 1 " ;

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    if (filtro.autoDeposito != "")
                    {
                        sql += " and pdep.auto_deposito=@autoDeposito ";
                        p1.ParameterName = "@autoDeposito";
                        p1.Value = filtro.autoDeposito;
                    }
                    if (filtro.autoDepartamento != "")
                    {
                        sql += " and p.auto_departamento=@autoDepartamento ";
                        p2.ParameterName = "@autoDepartamento";
                        p2.Value = filtro.autoDepartamento;
                    }
                    if (filtro.filtrarPor!= DtoLibInventario.Visor.Existencia.Enumerados.enumFiltrarPor.SinDefinir )
                    {
                        switch (filtro.filtrarPor) 
                        {
                            case DtoLibInventario.Visor.Existencia.Enumerados.enumFiltrarPor.ExistenciaMayorCero:
                                sql += " and (pdep.fisica>0) ";
                                break;

                            case DtoLibInventario.Visor.Existencia.Enumerados.enumFiltrarPor.ExistenciaIgualCero:
                                sql += " and (pdep.fisica=0) ";
                                break;

                            case DtoLibInventario.Visor.Existencia.Enumerados.enumFiltrarPor.ExistenciaMenorCero:
                                sql += " and (pdep.fisica<0) ";
                                break;

                            case DtoLibInventario.Visor.Existencia.Enumerados.enumFiltrarPor.ExistenciaPorDebajoNivelMinimo:
                                sql += " and (nivel_minimo>fisica AND nivel_minimo>0) ";
                                break;
                        }
                    }

                    var lst  = cnn.Database.SqlQuery<DtoLibInventario.Visor.Existencia.Ficha>(sql, p1, p2).ToList();
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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Visor.CostoEddad.Ficha> Visor_CostoEdad(DtoLibInventario.Visor.CostoEddad.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Visor.CostoEddad.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                    var sql = "SELECT p.nombre as nombrePrd, p.codigo as codigoPrd, p.auto as autoPrd, p.divisa as costoDivisa, " +
                        "p.contenido_compras as contenidoCompras, "+
                        "case when p.estatus='Activo' then '0' else '1' end as estatusActivo, " +
                        "p.estatus_cambio as estatusSuspendido, " +
                        "case when p.estatus_pesado='0' then 'N' when p.estatus_pesado='1' then 'S' end as esPesado, " +
                        "case when p.estatus_divisa='0' then 'N' when p.estatus_divisa='1' then 'S' end as esAdmDivisa, " +
                        "pdep.fisica as cntFisica, pdep.nivel_minimo as nivelMinimo, pdep.nivel_optimo as nivelOptimo, " +
                        "edep.auto as autoDeposito, edep.nombre as nombreDeposito, edep.codigo as codigoDeposito, " +
                        "edepart.auto as autoDepart, edepart.codigo as codigoDepart, edepart.nombre as nombreDepart, " +
                        "pmed.decimales, p.costo_und as costoUnd, p.fecha_ult_costo as fechaUltActCosto, p.fecha_ult_venta as fechaUltVenta  " +
                        "FROM `productos_deposito` as pdep " +
                        "join empresa_depositos as edep on pdep.auto_deposito=edep.auto " +
                        "join productos as p on pdep.auto_producto=p.auto " +
                        "join empresa_departamentos as edepart on p.auto_departamento=edepart.auto " +
                        "join productos_medida as pmed on p.auto_empaque_compra=pmed.auto " +
                        "WHERE 1 = 1 and (pdep.fisica<>0) ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    if (filtro.autoDepartamento != "")
                    {
                        sql += " and p.auto_departamento=@autoDepartamento ";
                        p1.ParameterName = "@autoDepartamento";
                        p1.Value = filtro.autoDepartamento;
                    }

                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Visor.CostoEddad.FichaDetalle>(sql, p1).ToList();
                    rt.Entidad = new DtoLibInventario.Visor.CostoEddad.Ficha();
                    rt.Entidad.fechaServidor= fechaSistema.Date;
                    rt.Entidad.detalles=lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Visor.Traslado.Ficha> Visor_Traslado(DtoLibInventario.Visor.Traslado.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Visor.Traslado.Ficha>();

            try
            {
                var list = new List<DtoLibInventario.Visor.Traslado.Ficha>();
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q= cnn.productos_movimientos_detalle.
                        Join(cnn.productos_movimientos, pmd=> pmd.auto_documento, pm=>pm.auto, (pmd,pm) => new {pmd,pm}).
                        Where(w=>w.pm.fecha.Year==filtro.ano && w.pm.estatus_anulado=="0" && w.pm.tipo=="03").
                        ToList();

                    if (filtro.mes != 0) 
                    {
                        q = q.Where(w => w.pm.fecha.Month == filtro.mes).ToList();
                    }

                    if (q != null) 
                    {
                        if (q.Count > 0) 
                        {
                            list = q.Select(s =>
                            {
                                var rg = new DtoLibInventario.Visor.Traslado.Ficha()
                                {
                                    autoDepositoDestino = s.pm.auto_destino,
                                    autoDepositoOrigen = s.pm.auto_deposito,
                                    autoPrd = s.pmd.auto_producto,
                                    autoUsuario = s.pm.auto_usuario,
                                    cantidad = s.pmd.cantidad_und,
                                    codigoDepositoDestino = s.pm.codigo_destino,
                                    codigoDepositoOrigen = s.pm.codigo_deposito,
                                    codigoPrd = s.pmd.codigo,
                                    codigoUsuario = s.pm.codigo_usuario,
                                    decimales = s.pmd.decimales,
                                    documentoNombre = s.pm.documento_nombre,
                                    documentoNro = s.pm.documento,
                                    fecha = s.pm.fecha,
                                    hora = s.pm.hora,
                                    nombreDepositoDestino = s.pm.destino,
                                    nombreDepositoOrigen = s.pm.deposito,
                                    nombrePrd = s.pmd.nombre,
                                    nombreUsuario = s.pm.usuario,
                                    nota = s.pm.nota,
                                };
                                return rg;
                            }).ToList();
                        }
                    }
                }
                rt.Lista = list;
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Visor.Ajuste.Ficha> Visor_Ajuste(DtoLibInventario.Visor.Ajuste.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Visor.Ajuste.Ficha>();

            try
            {
                var list = new List<DtoLibInventario.Visor.Ajuste.FichaDetalle>();
                var totalVentasNeta = 0.0m;
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", filtro.mes.ToString().Trim().PadLeft(2, '0'));
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", filtro.ano.ToString().Trim().PadLeft(4, '0'));
                    var sql = "select sum(neto*signo) as total from ventas "+
                        "where mes_relacion=@p1 and ano_relacion=@p2 "+
                        "and (tipo='01' or tipo='02' or tipo='03') "+
                        "and estatus_anulado='0' ";
                    var total= cnn.Database.SqlQuery<decimal?>(sql, p1, p2).FirstOrDefault();
                    if (total.HasValue)
                        totalVentasNeta = total.Value;

                    var q = cnn.productos_movimientos_detalle.
                        Join(cnn.productos_movimientos, pmd => pmd.auto_documento, pm => pm.auto, (pmd, pm) => new { pmd, pm }).
                        Where(w => w.pm.fecha.Year == filtro.ano && w.pm.estatus_anulado == "0" && 
                            (w.pm.tipo == "04" || ((w.pm.tipo=="01" || w.pm.tipo=="02") && w.pm.auto_concepto=="0000000007"))
                            ).ToList();

                    if (filtro.mes != 0)
                    {
                        q = q.Where(w => w.pm.fecha.Month == filtro.mes).ToList();
                    }

                    if (q != null)
                    {
                        if (q.Count > 0)
                        {
                            list = q.Select(s =>
                            {
                                var rg = new DtoLibInventario.Visor.Ajuste.FichaDetalle()
                                {
                                    autoDepositoOrigen = s.pm.auto_deposito,
                                    autoPrd = s.pmd.auto_producto,
                                    autoUsuario = s.pm.auto_usuario,
                                    cantidadUnd = s.pmd.cantidad_und,
                                    codigoDepositoOrigen = s.pm.codigo_deposito,
                                    codigoPrd = s.pmd.codigo,
                                    codigoUsuario = s.pm.codigo_usuario,
                                    decimales = s.pmd.decimales,
                                    documentoNro = s.pm.documento,
                                    fecha = s.pm.fecha,
                                    hora = s.pm.hora,
                                    nombreDepositoOrigen = s.pm.deposito,
                                    nombrePrd = s.pmd.nombre,
                                    nombreUsuario = s.pm.usuario,
                                    nota = s.pm.nota,
                                    costoUnd = s.pmd.costo_und,
                                    importe = s.pmd.total,
                                    signo = s.pmd.signo,
                                };
                                return rg;
                            }).ToList();
                        }
                    }
                }
                rt.Entidad = new DtoLibInventario.Visor.Ajuste.Ficha();
                rt.Entidad.detalles = list;
                rt.Entidad.montoVentaNeto = totalVentasNeta;
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Visor.CostoExistencia.Ficha> Visor_CostoExistencia(DtoLibInventario.Visor.CostoExistencia.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Visor.CostoExistencia.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = "SELECT p.nombre as nombrePrd, p.codigo as codigoPrd, p.auto as autoPrd, p.divisa as costoDivisa, " +
                        "p.contenido_compras as contenidoCompras, "+
                        "case when p.estatus='Activo' then '0' else '1' end as estatusActivo, "+
                        "p.estatus_cambio as estatusSuspendido, "+
                        "case when p.estatus_pesado='0' then 'N' when p.estatus_pesado='1' then 'S' end as esPesado, " +
                        "case when p.estatus_divisa='0' then 'N' when p.estatus_divisa='1' then 'S' end as esAdmDivisa, " +
                        "pdep.fisica as cntFisica, pdep.nivel_minimo as nivelMinimo, pdep.nivel_optimo as nivelOptimo, " +
                        "edep.auto as autoDeposito, edep.nombre as nombreDeposito, edep.codigo as codigoDeposito, " +
                        "edepart.auto as autoDepart, edepart.codigo as codigoDepart, edepart.nombre as nombreDepart, " +
                        "pmed.decimales, p.costo_und as costoUnd, p.fecha_ult_costo as fechaUltActCosto, p.fecha_ult_venta as fechaUltVenta  " +
                        "FROM `productos_deposito` as pdep " +
                        "join empresa_depositos as edep on pdep.auto_deposito=edep.auto " +
                        "join productos as p on pdep.auto_producto=p.auto " +
                        "join empresa_departamentos as edepart on p.auto_departamento=edepart.auto " +
                        "join productos_medida as pmed on p.auto_empaque_compra=pmed.auto " +
                        "WHERE 1 = 1 and (pdep.fisica<>0) ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    if (filtro.autoDepartamento != "")
                    {
                        sql += " and p.auto_departamento=@autoDepartamento ";
                        p1.ParameterName = "@autoDepartamento";
                        p1.Value = filtro.autoDepartamento;
                    }

                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    if (filtro.autoDeposito != "")
                    {
                        sql += " and pdep.auto_deposito=@autoDeposito ";
                        p2.ParameterName = "@autoDeposito";
                        p2.Value = filtro.autoDeposito;
                    }

                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Visor.CostoExistencia.Ficha>(sql, p1,p2).ToList();
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
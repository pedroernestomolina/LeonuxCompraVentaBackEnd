using LibEntityPos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvPos
{

    public partial class Provider: IPos.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibPos.Cliente.Lista.Ficha> Cliente_GetLista(DtoLibPos.Cliente.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Cliente.Lista.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = " select auto, codigo, ci_rif as ciRif, razon_social as nombre, estatus ";
                    var sql_2 = " from clientes ";
                    var sql_3 = " where 1=1 ";
                    var sql_4 = "";

                    var p1 = new MySqlParameter();
                    if (filtro.cadena != "")
                    {
                        var valor = "";
                        if (filtro.preferenciaBusqueda == DtoLibPos.Cliente.Lista.Enumerados.enumPreferenciaBusqueda.Codigo)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql_3 += " and codigo like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql_3 += " and codigo like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.preferenciaBusqueda == DtoLibPos.Cliente.Lista.Enumerados.enumPreferenciaBusqueda.Nombre)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql_3 += " and razon_social like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql_3 += " and razon_social like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.preferenciaBusqueda == DtoLibPos.Cliente.Lista.Enumerados.enumPreferenciaBusqueda.CiRif)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql_3 += " and ci_rif like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql_3 += " and ci_rif like @p";
                                valor = cad + "%";
                            }
                        }
                        p1.ParameterName = "@p";
                        p1.Value = valor;

                        //if (filtro.preferenciaBusqueda == DtoLibPos.Cliente.Lista.Enumerados.enumPreferenciaBusqueda.Nombre)
                        //{
                        //    sql_3 += " and razon_social like @p1 ";
                        //    p1.ParameterName = "p1";
                        //    p1.Value = filtro.cadena + "%";
                        //}
                    }
                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Cliente.Lista.Ficha>(sql, p1).ToList();
                    result.Lista = lst;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Cliente.Entidad.Ficha> Cliente_GetFichaById(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Cliente.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.clientes.Find(id);
                    if (ent == null)
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "[ ID ] CLIENTE NO ENCONTRADO";
                        return result;
                    }

                    var nr = new DtoLibPos.Cliente.Entidad.Ficha()
                    {
                        CiRif = ent.ci_rif,
                        Codigo = ent.codigo,
                        DireccionFiscal = ent.dir_fiscal,
                        Id = ent.auto,
                        Nombre = ent.razon_social,
                        Telefono = ent.telefono,
                        estatus= ent.estatus,
                    };
                    result.Entidad = nr;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Cliente.Entidad.Ficha> Cliente_GetFichaByCiRif(string ciRif)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Cliente.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.clientes.FirstOrDefault(f => f.ci_rif.Trim().ToUpper() == ciRif.Trim().ToUpper());
                    if (ent == null)
                    {
                        result.Entidad = new DtoLibPos.Cliente.Entidad.Ficha();
                        return result;
                    }

                    var nr = new DtoLibPos.Cliente.Entidad.Ficha()
                    {
                        CiRif = ent.ci_rif,
                        Codigo = ent.codigo,
                        DireccionFiscal = ent.dir_fiscal,
                        Id = ent.auto,
                        Nombre = ent.razon_social,
                        Telefono = ent.telefono,
                        estatus=ent.estatus,
                    };
                    result.Entidad = nr;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoAuto Cliente_Agregar(DtoLibPos.Cliente.Agregar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var ctx = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var r = ctx.Database.ExecuteSqlCommand("update sistema_contadores set a_clientes=a_clientes+1");
                        if (r == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR CONTADOR DE CLIENTE";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var fechaSistema = ctx.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var cntCliente = ctx.Database.SqlQuery<int>("select a_clientes from sistema_contadores").FirstOrDefault();
                        var AutoCliente = cntCliente.ToString().Trim().PadLeft(10, '0');
                        var fechaNula = new DateTime(2000, 01, 01);

                        var ent = new clientes()
                        {
                            auto = AutoCliente,
                            auto_grupo = ficha.autoGrupo,
                            auto_zona = ficha.autoZona,
                            auto_estado = ficha.autoEstado,
                            auto_agencia = ficha.autoAgencia,
                            auto_cobrador = ficha.autoCobrador,
                            auto_vendedor = ficha.autoVendedor,
                            auto_codigo_anticipos = ficha.autoCodigoAnticipos,
                            auto_codigo_cobrar = ficha.autoCodigoCobrar,
                            auto_codigo_ingresos = ficha.autoCodigoIngreso,

                            ci_rif = ficha.ciRif,
                            razon_social = ficha.razonSocial,
                            dir_fiscal = ficha.dirFiscal,
                            telefono = ficha.telefono,
                            estatus = ficha.estatus,
                            estatus_credito = ficha.estatusCredito,
                            categoria = ficha.categoria,
                            tarifa = ficha.tarifa,
                            dias_credito = ficha.diasCredito,
                            limite_credito = ficha.limiteCredito,
                            doc_pendientes = ficha.docPendientes,
                            pais = ficha.pais,
                            fecha_alta = fechaSistema.Date,
                            denominacion_fiscal = ficha.denominacionFiscal,

                            codigo = ficha.codigo,
                            nombre = ficha.nombre,
                            dir_despacho = ficha.dirDespacho,
                            contacto = ficha.contacto,
                            email = ficha.email,
                            website = ficha.webSite,
                            codigo_postal = ficha.codigoPostal,
                            retencion_iva = ficha.retencionIva,
                            retencion_islr = ficha.retencionIslr,
                            descuento = ficha.descuento,
                            recargo = ficha.recargo,
                            estatus_morosidad = ficha.estatusMorosidad,
                            estatus_lunes = ficha.estatusLunes,
                            estatus_martes = ficha.estatusMartes,
                            estatus_miercoles = ficha.estatusMiercoles,
                            estatus_jueves = ficha.estatusJueves,
                            estatus_viernes = ficha.estatusViernes,
                            estatus_sabado = ficha.estatusSabado,
                            estatus_domingo = ficha.estatusDomingo,
                            fecha_baja = fechaNula,
                            fecha_ult_pago = fechaNula,
                            fecha_ult_venta = fechaNula,
                            anticipos = ficha.anticipos,
                            debitos = ficha.debitos,
                            creditos = ficha.creditos,
                            saldo = ficha.saldo,
                            disponible = ficha.disponible,
                            memo = ficha.memo,
                            aviso = ficha.aviso,
                            cuenta = ficha.cuenta,
                            iban = ficha.iban,
                            swit = ficha.swit,
                            dir_banco = ficha.dirBanco,
                            descuento_pronto_pago = ficha.descuentoProntoPago,
                            importe_ult_pago = ficha.importeUltPago,
                            importe_ult_venta = ficha.importeUltVenta,
                            telefono2 = ficha.telefono2,
                            fax = ficha.fax,
                            celular = ficha.celular,
                            abc=ficha.abc,
                            fecha_clasificacion=fechaNula,
                            monto_clasificacion=ficha.montoClasificacion,
                        };
                        ctx.clientes.Add(ent);
                        ctx.SaveChanges();

                        ts.Complete();
                        result.Auto = AutoCliente;
                    }
                }
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
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }

        public DtoLib.ResultadoLista<DtoLibPos.Cliente.Documento.Ficha> Cliente_Documento_GetLista(DtoLibPos.Cliente.Documento.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Cliente.Documento.Ficha>();

            try
            {
                using (var cnn= new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"SELECT v.fecha, v.documento, v.total as monto, 
                                    v.monto_divisa as montoDivisa, v.factor_cambio as tasaDivisa, 
                                    v.estatus_anulado as estatus, v.tipo as codTipoDoc, v.serie, 
                                    v.signo, v.documento_nombre as nombreTipoDoc  ";
                    var sql_2 = " FROM ventas as v";
                    var sql_3 = " where v.auto_cliente=@p1 and v.fecha>=@p2 and v.fecha<=@p3 ";
                    var sql_4 = "";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@p1";
                    p1.Value = filtro.autoCliente;
                    p2.ParameterName = "@p2";
                    p2.Value = filtro.desde;
                    p3.ParameterName = "@p3";
                    p3.Value = filtro.hasta;

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibPos.Cliente.Documento.Ficha>(sql, p1, p2, p3, p4).ToList();
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

        public DtoLib.ResultadoLista<DtoLibPos.Cliente.Articulos.Ficha> Cliente_ArticuloVenta_GetLista(DtoLibPos.Cliente.Articulos.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Cliente.Articulos.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"SELECT p.codigo as codigoPrd, p.nombre as nombrePrd, v.fecha, v.documento, 
                            vd.cantidad, vd.cantidad_und as cantUnd, vd.empaque, vd.estatus_anulado as estatus, 
                            vd.contenido_empaque as contenidoEmp, v.tipo as codTipoDoc, v.serie, v.factor_cambio as tasaCambio, 
                            vd.precio_und as precioUnd, v.signo, v.documento_nombre as nombreTipoDoc ";

                    var sql_2 = @" FROM ventas_detalle as vd 
                                join productos as p on vd.auto_producto=p.auto 
                                join ventas as v on vd.auto_documento=v.auto ";

                    var sql_3 = " where v.auto_cliente=@p1 and v.fecha>=@p2 and v.fecha<=@p3 ";
                    var sql_4 = "";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@p1";
                    p1.Value = filtro.autoCliente;
                    p2.ParameterName = "@p2";
                    p2.Value = filtro.desde;
                    p3.ParameterName = "@p3";
                    p3.Value = filtro.hasta;

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibPos.Cliente.Articulos.Ficha>(sql, p1, p2, p3, p4).ToList();
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
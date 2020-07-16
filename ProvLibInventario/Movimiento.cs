using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Traslado.Consultar.ProductoPorDebajoNivelMinimo> Producto_Movimiento_Traslado_Consultar_ProductosPorDebajoNivelMinimo(DtoLibInventario.Movimiento.Traslado.Consultar.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Traslado.Consultar.ProductoPorDebajoNivelMinimo>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q = cnn.productos_deposito.ToList();
                    q = q.Where(f => f.fisica < f.nivel_minimo && f.productos.estatus.Trim().ToUpper()=="ACTIVO").ToList();
                    if (filtro.autoDeposito != "") 
                    {
                        q = q.Where(f => f.auto_deposito == filtro.autoDeposito).ToList();
                    }

                    var list = new List<DtoLibInventario.Movimiento.Traslado.Consultar.ProductoPorDebajoNivelMinimo>();
                    if (q != null) 
                    {
                        if (q.Count() > 0) 
                        {
                            list = q.Select(s =>
                            {
                                var _decimales = "";
                                var _empaque = "";
                                var ent = cnn.productos_medida.Find(s.productos.auto_empaque_compra);
                                if (ent != null) 
                                {
                                    _decimales = ent.decimales;
                                    _empaque = ent.nombre;
                                }

                                var nr = new DtoLibInventario.Movimiento.Traslado.Consultar.ProductoPorDebajoNivelMinimo()
                                {
                                    autoProducto = s.auto_producto,
                                    autoDepartamento = s.productos.auto_departamento,
                                    autoGrupo = s.productos.auto_grupo,
                                    categoria = s.productos.categoria,
                                    codigoProducto = s.productos.codigo,
                                    contenidEmpCompra = s.productos.contenido_compras,
                                    costoFinalCompra = s.productos.costo,
                                    costoFinalUnd = s.productos.costo_und,
                                    cntUndReponer = s.nivel_optimo - s.fisica,
                                    decimales = _decimales,
                                    empaqueCompra = _empaque,
                                    nombreProducto = s.productos.nombre,
                                };
                                return nr;
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

        public DtoLib.ResultadoAuto Producto_Movimiento_Traslado_Insertar(DtoLibInventario.Movimiento.Traslado.Insertar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "";

                        sql = "update sistema_contadores set a_productos_movimientos=a_productos_movimientos+1, a_productos_movimientos_traslados=a_productos_movimientos_traslados+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aMov = cnn.Database.SqlQuery<int>("select a_productos_movimientos from sistema_contadores").FirstOrDefault();
                        var aMovTraslado = cnn.Database.SqlQuery<int>("select a_productos_movimientos_traslados from sistema_contadores").FirstOrDefault();
                        var autoMov = aMov.ToString().Trim().PadLeft(10, '0');
                        var numDoc = aMovTraslado.ToString().Trim().PadLeft(10, '0');
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var entMov = new productos_movimientos()
                        {
                            auto= autoMov,
                            auto_concepto = ficha.autoConcepto,
                            auto_deposito = ficha.autoDepositoOrigen,
                            auto_destino = ficha.autoDepositoDestino,
                            auto_remision = ficha.remision,
                            auto_usuario = ficha.autoUsuario,
                            autorizado = ficha.autorizado,
                            cierre_ftp = ficha.cierreFtp,
                            codigo_concepto = ficha.codConcepto,
                            codigo_deposito = ficha.codDepositoOrigen,
                            codigo_destino = ficha.codDepositoDestino,
                            codigo_sucursal = ficha.codigoSucursal,
                            codigo_usuario = ficha.codUsuario,
                            concepto = ficha.desConcepto,
                            deposito = ficha.desDepositoOrigen,
                            destino = ficha.desDepositoDestino,
                            documento = numDoc,
                            documento_nombre = ficha.documentoNombre,
                            estacion = ficha.estacion,
                            estatus_anulado = ficha.estatusAnulado,
                            estatus_cierre_contable = ficha.estatusCierreContable,
                            fecha = fechaSistema.Date,
                            hora = fechaSistema.ToShortTimeString(),
                            nota = ficha.nota,
                            renglones = ficha.renglones,
                            situacion = ficha.situacion,
                            tipo = ficha.tipo,
                            total = ficha.total,
                            usuario = ficha.usuario,
                        };
                        cnn.productos_movimientos.Add(entMov);
                        cnn.SaveChanges();

                        var _auto=0;
                        foreach (var det in ficha.detalles) 
                        {
                            _auto+=1;
                            var entMovDet = new productos_movimientos_detalle()
                            {
                                auto = _auto.ToString().Trim().PadLeft(10, '0'),
                                auto_departamento = det.autoDepartamento,
                                auto_documento = autoMov,
                                auto_grupo = det.autoGrupo,
                                auto_producto = det.autoProducto,
                                cantidad = det.cantidad,
                                cantidad_bono = det.cantidadBono,
                                cantidad_und = det.cantidadUnd,
                                categoria = det.categoria,
                                cierre_ftp = ficha.cierreFtp ,
                                codigo = det.codigoProducto,
                                contenido_empaque = det.contEmpaque,
                                costo_compra = det.costoCompra,
                                costo_und = det.costoUnd,
                                decimales = det.decimales,
                                empaque = det.empaque,
                                estatus_anulado = det.estatusAnulado,
                                estatus_unidad = det.estatusUnidad,
                                existencia = 0,
                                fecha = fechaSistema.Date,
                                fisica = 0,
                                nombre = det.nombreProducto,
                                signo = det.signo,
                                tipo = det.tipo,
                                total = det.total,
                            };
                            cnn.productos_movimientos_detalle.Add(entMovDet);
                            cnn.SaveChanges();
                        };


                        var sql2=@"INSERT INTO productos_kardex (auto_producto,total,auto_deposito,auto_concepto,auto_documento,
                            fecha,hora,documento,modulo,entidad,signo,cantidad,cantidad_bono,cantidad_und,costo_und,estatus_anulado,
                            nota,precio_und,codigo,siglas,codigo_sucursal, cierre_fpt) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, 
                            {12}, {13}, {14}, {15},{16}, {17}, {18}, {19}, {20}, {21})";
                        //KARDEX MOV=> ITEMS
                        foreach (var dt in ficha.movKardex)
                        {
                            var vk = cnn.Database.ExecuteSqlCommand(sql2, dt.autoProducto, dt.total, dt.autoDeposito ,
                                dt.autoConcepto, autoMov, fechaSistema.Date, fechaSistema.ToShortTimeString(), numDoc,
                                dt.modulo ,dt.entidad, dt.signo ,dt.cantidad, dt.cantidadBono, dt.cantidadUnd, dt.costoUnd , dt.estatusAnulado,
                                dt.nota, dt.precioUnd, dt.codigo ,dt.siglas, dt.codigoSucursal, ficha.cierreFtp);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO KARDEX [ " + Environment.NewLine + dt.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };
                        
                        ts.Complete();
                        result.Auto = autoMov;
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
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries )
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
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

    }

}
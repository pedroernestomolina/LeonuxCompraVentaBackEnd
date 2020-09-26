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
                    q = q.Where(f => f.fisica < f.nivel_minimo && f.productos.estatus.Trim().ToUpper() == "ACTIVO").ToList();
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
                                //var ent = cnn.productos_medida.Find(s.productos.auto_empaque_compra);
                                var ent = s.productos.productos_medida2;
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
                            auto_remision = ficha.autoRemision,
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


                        var sql1 = @"INSERT INTO productos_movimientos_detalle (auto_documento, auto_producto, codigo, nombre, " +
                            "cantidad, cantidad_bono, cantidad_und, categoria, fecha, tipo, estatus_anulado, contenido_empaque, " +
                            "empaque, decimales, auto, costo_und, total, costo_compra, estatus_unidad, signo, existencia, " +
                            "fisica, auto_departamento, auto_grupo, cierre_ftp) " +
                            "VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, "+
                            "{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24})";

                        var _auto=0;
                        foreach (var det in ficha.detalles) 
                        {
                            _auto+=1;
                            var xauto = _auto.ToString().Trim().PadLeft(10, '0');

                            var vk = cnn.Database.ExecuteSqlCommand(sql1, autoMov, det.autoProducto, det.codigoProducto, det.nombreProducto, 
                                det.cantidad, det.cantidadBono, det.cantidadUnd, det.categoria, fechaSistema.Date, det.tipo,
                                det.estatusAnulado,det.contEmpaque, det.empaque, det.decimales, xauto, det.costoUnd, det.total,
                                det.costoCompra, det.estatusUnidad, det.signo, 0, 0, det.autoDepartamento, det.autoGrupo, ficha.cierreFtp);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO DETALLE [ " + Environment.NewLine + det.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };

                        //ACTUALIZAR DEPOSITO-ENTRADA MERCANCIA
                        foreach (var dt in ficha.prdDeposito)
                        {
                            var entPrdDepOrigen = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == dt.autoProducto && f.auto_deposito == dt.autoDepositoOrigen);
                            if (entPrdDepOrigen == null)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ PRODUCTO - DEPOSITO ORIGEN ] NO ENCONTRADO " + Environment.NewLine + "Producto: " + dt.autoProducto + ", Deposito: " + dt.autoDepositoOrigen;
                                return result;
                            }
                            entPrdDepOrigen.fisica -= dt.cantidadUnd;
                            entPrdDepOrigen.disponible = entPrdDepOrigen.fisica;
                            cnn.SaveChanges();

                            var entPrdDepDestino = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == dt.autoProducto && f.auto_deposito == dt.autoDepositoDestino);
                            if (entPrdDepDestino == null)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ PRODUCTO - DEPOSITO DESTINO ] NO ENCONTRADO " + Environment.NewLine + "Producto: " + dt.autoProducto + ", Deposito: " + dt.autoDepositoOrigen;
                                return result;
                            }
                            entPrdDepDestino.fisica += dt.cantidadUnd;
                            entPrdDepDestino.disponible = entPrdDepDestino.fisica;
                            cnn.SaveChanges();
                        };


                        //KARDEX MOV=> ITEMS
                        var sql2 = @"INSERT INTO productos_kardex (auto_producto,total,auto_deposito,auto_concepto,auto_documento,
                            fecha,hora,documento,modulo,entidad,signo,cantidad,cantidad_bono,cantidad_und,costo_und,estatus_anulado,
                            nota,precio_und,codigo,siglas,codigo_sucursal, cierre_ftp, codigo_deposito, nombre_deposito, 
                            codigo_concepto, nombre_concepto) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, 
                            {12}, {13}, {14}, {15},{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25})";
                        foreach (var dt in ficha.movKardex)
                        {
                            var vk = cnn.Database.ExecuteSqlCommand(sql2, dt.autoProducto, dt.total, dt.autoDeposito ,
                                dt.autoConcepto, autoMov, fechaSistema.Date, fechaSistema.ToShortTimeString(), numDoc,
                                dt.modulo ,dt.entidad, dt.signo ,dt.cantidad, dt.cantidadBono, dt.cantidadUnd, dt.costoUnd , dt.estatusAnulado,
                                dt.nota, dt.precioUnd, dt.codigo ,dt.siglas, dt.codigoSucursal, ficha.cierreFtp, dt.codigoDeposito,
                                dt.nombreDeposito, dt.codigoConcepto, dt.nombreConcepto);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO KARDEX [ " + Environment.NewLine + dt.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };
                        
                        ts.Complete();
                        result.Auto = autoMov ;
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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Ver.Ficha> Producto_Movimiento_GetFicha(string autoDoc)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Ver.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.productos_movimientos.Find(autoDoc);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] DOCUMENTO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    var entDet = cnn.productos_movimientos_detalle.Where(f => f.auto_documento == autoDoc).ToList();
                    var nr = new DtoLibInventario.Movimiento.Ver.Ficha()
                    {
                        autorizadoPor = ent.autorizado,
                        codigoConcepto = ent.codigo_concepto,
                        codigoDepositoDestino = ent.codigo_destino,
                        codigoDepositoOrigen = ent.codigo_deposito,
                        concepto = ent.concepto,
                        depositoDestino = ent.destino,
                        depositoOrigen = ent.deposito,
                        documentoNro = ent.documento,
                        estacion = ent.estacion,
                        fecha = ent.fecha,
                        hora = ent.hora,
                        notas = ent.nota,
                        tipoDocumento = ent.tipo,
                        total = ent.total,
                        usuario = ent.usuario,
                        usuarioCodigo = ent.codigo_usuario,
                    };

                    var det = entDet.Select(s =>
                    {
                        var dt = new DtoLibInventario.Movimiento.Ver.Detalle()
                        {
                            cantidad = s.cantidad_und,
                            codigo = s.codigo,
                            costoUnd = s.costo_und,
                            descripcion = s.nombre,
                            importe = s.total,
                            signo = s.signo,
                        };
                        return dt;
                    }).ToList();
                    nr.detalles = det;

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

        public DtoLib.ResultadoAuto Producto_Movimiento_Cargo_Insertar(DtoLibInventario.Movimiento.Cargo.Insertar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "";

                        sql = "update sistema_contadores set a_productos_movimientos=a_productos_movimientos+1, a_productos_movimientos_cargos=a_productos_movimientos_cargos+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aMov = cnn.Database.SqlQuery<int>("select a_productos_movimientos from sistema_contadores").FirstOrDefault();
                        var aMovCargo = cnn.Database.SqlQuery<int>("select a_productos_movimientos_cargos from sistema_contadores").FirstOrDefault();
                        var autoMov = aMov.ToString().Trim().PadLeft(10, '0');
                        var numDoc = aMovCargo.ToString().Trim().PadLeft(10, '0');
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var entMov = new productos_movimientos()
                        {
                            auto = autoMov,
                            auto_concepto = ficha.autoConcepto,
                            auto_deposito = ficha.autoDepositoOrigen,
                            auto_destino = ficha.autoDepositoDestino,
                            auto_remision = ficha.autoRemision,
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


                        var sql1 = @"INSERT INTO productos_movimientos_detalle (auto_documento, auto_producto, codigo, nombre, " +
                            "cantidad, cantidad_bono, cantidad_und, categoria, fecha, tipo, estatus_anulado, contenido_empaque, " +
                            "empaque, decimales, auto, costo_und, total, costo_compra, estatus_unidad, signo, existencia, " +
                            "fisica, auto_departamento, auto_grupo, cierre_ftp) " +
                            "VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, " +
                            "{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24})";

                        var _auto = 0;
                        foreach (var det in ficha.detalles)
                        {
                            _auto += 1;
                            var xauto = _auto.ToString().Trim().PadLeft(10, '0');

                            var vk = cnn.Database.ExecuteSqlCommand(sql1, autoMov, det.autoProducto, det.codigoProducto, det.nombreProducto,
                                det.cantidad, det.cantidadBono, det.cantidadUnd, det.categoria, fechaSistema.Date, det.tipo,
                                det.estatusAnulado, det.contEmpaque, det.empaque, det.decimales, xauto, det.costoUnd, det.total,
                                det.costoCompra, det.estatusUnidad, det.signo, 0, 0, det.autoDepartamento, det.autoGrupo, ficha.cierreFtp);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO DETALLE [ " + Environment.NewLine + det.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };

                        //KARDEX MOV=> ITEMS
                        var sql2 = @"INSERT INTO productos_kardex (auto_producto,total,auto_deposito,auto_concepto,auto_documento,
                            fecha,hora,documento,modulo,entidad,signo,cantidad,cantidad_bono,cantidad_und,costo_und,estatus_anulado,
                            nota,precio_und,codigo,siglas,codigo_sucursal, cierre_ftp, codigo_deposito, nombre_deposito, 
                            codigo_concepto, nombre_concepto) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, 
                            {12}, {13}, {14}, {15},{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25})";
                        foreach (var dt in ficha.movKardex)
                        {
                            var vk = cnn.Database.ExecuteSqlCommand(sql2, dt.autoProducto, dt.total, dt.autoDeposito,
                                dt.autoConcepto, autoMov, fechaSistema.Date, fechaSistema.ToShortTimeString(), numDoc,
                                dt.modulo, dt.entidad, dt.signoMov, dt.cantidad, dt.cantidadBono, dt.cantidadUnd, dt.costoUnd, dt.estatusAnulado,
                                dt.nota, dt.precioUnd, dt.codigoMov, dt.siglasMov, dt.codigoSucursal, ficha.cierreFtp, dt.codigoDeposito,
                                dt.nombreDeposito, dt.codigoConcepto, dt.nombreConcepto);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO KARDEX [ " + Environment.NewLine + dt.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cnn.SaveChanges();
                        };


                        //ACTUALIZAR COSTO
                        foreach (var dt in ficha.prdCosto) 
                        {
                            var entPrd = cnn.productos.Find(dt.autoProducto);
                            if (entPrd == null)
                            {
                                result.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO "+Environment.NewLine+dt.autoProducto;
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            var montoActualInvPromedioUnd = 0.0m;
                            var cp=dt.costoFinal;
                            var cpu=dt.costoFinalUnd;
                            var existenciaActualUnd = cnn.productos_deposito.Where(s=>s.auto_producto==dt.autoProducto).Sum(s=>s.fisica);
                            if (existenciaActualUnd  > 0) 
                            {
                                montoActualInvPromedioUnd = existenciaActualUnd * entPrd.costo_promedio_und;
                                var x1 = montoActualInvPromedioUnd + dt.importeEntradaUnd;
                                var x2 = existenciaActualUnd + dt.cantidadEntranteUnd;

                                cpu = x1 / x2 ;
                                cp = cpu / entPrd.contenido_compras;
                            }

                            entPrd.costo = dt.costoFinal;
                            entPrd.costo_und = dt.costoFinalUnd;
                            entPrd.costo_promedio = cp ;
                            entPrd.costo_promedio_und = cpu ;
                            entPrd.divisa = dt.costoDivisa;
                            entPrd.fecha_ult_costo = fechaSistema.Date;
                            entPrd.fecha_cambio = fechaSistema.Date;
                            cnn.SaveChanges();
                        }

                        //ACTUALIZAR DEPOSITO-ENTRADA MERCANCIA
                        foreach (var dt in ficha.prdDeposito)
                        {
                            var entPrdDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == dt.autoProducto && f.auto_deposito == dt.autoDeposito);
                            if (entPrdDep == null)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ PRODUCTO - DEPOSITO ] NO ENCONTRADO " + Environment.NewLine + "Producto: " + dt.autoProducto + ", Deposito: " + dt.autoDeposito;
                                return result;
                            }
                            entPrdDep.fisica += dt.cantidadUnd;
                            entPrdDep.disponible = entPrdDep.fisica;
                            cnn.SaveChanges();
                        };

                        // REGISTRAR HISTORICO COSTO
                        foreach (var dt in ficha.prdCostoHistorico)
                        {
                            var entPrdCostoHistorico = new productos_costos()
                            {
                                auto_producto = dt.autoProducto,
                                nota = dt.nota,
                                fecha = fechaSistema.Date,
                                estacion = ficha.estacion,
                                hora = fechaSistema.ToShortTimeString(),
                                usuario = ficha.usuario,
                                costo = dt.costo,
                                costo_divisa = dt.divisa,
                                divisa = dt.tasaCambio,
                                serie = dt.serie,
                                documento = numDoc,
                            };
                            cnn.productos_costos.Add(entPrdCostoHistorico);
                            cnn.SaveChanges();
                        }


                        // ACTUALIZAR PRECIOS 
                        foreach (var dt in ficha.prdPrecio)
                        {
                            var entPrd = cnn.productos.Find(dt.autoProducto);
                            if (entPrd == null)
                            {
                                result.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO " + Environment.NewLine + dt.autoProducto;
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            if (dt.precio_1 != null) 
                            {
                                entPrd.precio_1 = dt.precio_1.precioNeto;
                                entPrd.pdf_1 = dt.precio_1.precio_divisa_full;
                            }
                            if (dt.precio_2 != null)
                            {
                                entPrd.precio_2 = dt.precio_2.precioNeto;
                                entPrd.pdf_2 = dt.precio_2.precio_divisa_full;
                            }
                            if (dt.precio_3 != null)
                            {
                                entPrd.precio_3 = dt.precio_3.precioNeto;
                                entPrd.pdf_3 = dt.precio_3.precio_divisa_full;
                            }
                            if (dt.precio_4 != null)
                            {
                                entPrd.precio_4 = dt.precio_4.precioNeto;
                                entPrd.pdf_4 = dt.precio_4.precio_divisa_full;
                            }
                            if (dt.precio_5 != null)
                            {
                                entPrd.precio_pto = dt.precio_5 .precioNeto;
                                entPrd.pdf_pto = dt.precio_5 .precio_divisa_full;
                            }

                            cnn.SaveChanges();
                        }


                        // ACTUALIZAR MARGEN UTILIDAD
                        foreach (var dt in ficha.prdPrecioMargen)
                        {
                            var entPrd = cnn.productos.Find(dt.autoProducto);
                            if (entPrd == null)
                            {
                                result.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO " + Environment.NewLine + dt.autoProducto;
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            if (dt.precio_1 != null)
                            {
                                entPrd.utilidad_1 = dt.precio_1.utilidad;
                            }
                            if (dt.precio_2 != null)
                            {
                                entPrd.utilidad_2 = dt.precio_2.utilidad;
                            }
                            if (dt.precio_3 != null)
                            {
                                entPrd.utilidad_3 = dt.precio_3.utilidad;
                            }
                            if (dt.precio_4 != null)
                            {
                                entPrd.utilidad_4 = dt.precio_4.utilidad;
                            }
                            if (dt.precio_5 != null)
                            {
                                entPrd.utilidad_pto = dt.precio_5.utilidad;
                            }

                            cnn.SaveChanges();
                        }


                        // REGISTRAR HISTORICO PRECIO
                        foreach (var dt in ficha.prdPrecioHistorico)
                        {
                            var entPrdPrecioHistorico = new productos_precios()
                            {
                                auto_producto = dt.autoProducto,
                                nota = dt.nota,
                                fecha = fechaSistema.Date,
                                estacion = ficha.estacion,
                                hora = fechaSistema.ToShortTimeString(),
                                usuario = ficha.usuario,
                                precio_id=dt.precio_id,
                                precio=dt.precio,
                            };
                            cnn.productos_precios.Add(entPrdPrecioHistorico);
                            cnn.SaveChanges();
                        }

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
                foreach (var eve in e.Entries)
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

        public DtoLib.ResultadoAuto Producto_Movimiento_DesCargo_Insertar(DtoLibInventario.Movimiento.DesCargo.Insertar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "";

                        sql = "update sistema_contadores set a_productos_movimientos=a_productos_movimientos+1, a_productos_movimientos_descargos=a_productos_movimientos_descargos+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aMov = cnn.Database.SqlQuery<int>("select a_productos_movimientos from sistema_contadores").FirstOrDefault();
                        var aMovDesCargo = cnn.Database.SqlQuery<int>("select a_productos_movimientos_descargos from sistema_contadores").FirstOrDefault();
                        var autoMov = aMov.ToString().Trim().PadLeft(10, '0');
                        var numDoc = aMovDesCargo.ToString().Trim().PadLeft(10, '0');
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var entMov = new productos_movimientos()
                        {
                            auto = autoMov,
                            auto_concepto = ficha.autoConcepto,
                            auto_deposito = ficha.autoDepositoOrigen,
                            auto_destino = ficha.autoDepositoDestino,
                            auto_remision = ficha.autoRemision,
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


                        var sql1 = @"INSERT INTO productos_movimientos_detalle (auto_documento, auto_producto, codigo, nombre, " +
                            "cantidad, cantidad_bono, cantidad_und, categoria, fecha, tipo, estatus_anulado, contenido_empaque, " +
                            "empaque, decimales, auto, costo_und, total, costo_compra, estatus_unidad, signo, existencia, " +
                            "fisica, auto_departamento, auto_grupo, cierre_ftp) " +
                            "VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, " +
                            "{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24})";

                        var _auto = 0;
                        foreach (var det in ficha.detalles)
                        {
                            _auto += 1;
                            var xauto = _auto.ToString().Trim().PadLeft(10, '0');

                            var vk = cnn.Database.ExecuteSqlCommand(sql1, autoMov, det.autoProducto, det.codigoProducto, det.nombreProducto,
                                det.cantidad, det.cantidadBono, det.cantidadUnd, det.categoria, fechaSistema.Date, det.tipo,
                                det.estatusAnulado, det.contEmpaque, det.empaque, det.decimales, xauto, det.costoUnd, det.total,
                                det.costoCompra, det.estatusUnidad, det.signo, 0, 0, det.autoDepartamento, det.autoGrupo, ficha.cierreFtp);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO DETALLE [ " + Environment.NewLine + det.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };


                        //ACTUALIZAR DEPOSITO-ENTRADA MERCANCIA
                        foreach (var dt in ficha.prdDeposito)
                        {
                            var entPrdDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == dt.autoProducto && f.auto_deposito == dt.autoDeposito);
                            if (entPrdDep == null)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ PRODUCTO - DEPOSITO ] NO ENCONTRADO " + Environment.NewLine + "Producto: " + dt.autoProducto + ", Deposito: " + dt.autoDeposito;
                                return result;
                            }
                            entPrdDep.fisica -= dt.cantidadUnd;
                            entPrdDep.disponible = entPrdDep.fisica;
                            cnn.SaveChanges();
                        };


                        //KARDEX MOV=> ITEMS
                        var sql2 = @"INSERT INTO productos_kardex (auto_producto,total,auto_deposito,auto_concepto,auto_documento,
                            fecha,hora,documento,modulo,entidad,signo,cantidad,cantidad_bono,cantidad_und,costo_und,estatus_anulado,
                            nota,precio_und,codigo,siglas,codigo_sucursal, cierre_ftp, codigo_deposito, nombre_deposito, 
                            codigo_concepto, nombre_concepto) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, 
                            {12}, {13}, {14}, {15},{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25})";
                        foreach (var dt in ficha.movKardex)
                        {
                            var vk = cnn.Database.ExecuteSqlCommand(sql2, dt.autoProducto, dt.total, dt.autoDeposito,
                                dt.autoConcepto, autoMov, fechaSistema.Date, fechaSistema.ToShortTimeString(), numDoc,
                                dt.modulo, dt.entidad, dt.signoMov, dt.cantidad, dt.cantidadBono, dt.cantidadUnd, dt.costoUnd, dt.estatusAnulado,
                                dt.nota, dt.precioUnd, dt.codigoMov, dt.siglasMov, dt.codigoSucursal, ficha.cierreFtp, dt.codigoDeposito,
                                dt.nombreDeposito, dt.codigoConcepto, dt.nombreConcepto);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO KARDEX [ " + Environment.NewLine + dt.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cnn.SaveChanges();
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
                foreach (var eve in e.Entries)
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

        public DtoLib.ResultadoEntidad<bool> Producto_Movimiento_Verificar_ExistenciaDisponible(List<DtoLibInventario.Movimiento.Verificar.ExistenciaDisponible.Ficha> lista)
        {
            var rt = new DtoLib.ResultadoEntidad<bool>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    foreach (var rg in lista) 
                    {
                        rt.Entidad = true;
                        var ent = cnn.productos_deposito.FirstOrDefault(f=>f.auto_producto==rg.autoProducto && f.auto_deposito==rg.autoDeposito);
                        if (ent == null)
                        {
                            rt.Mensaje = "[ ID ] PRODUCTO / DEPOSITO NO ENCONTRADO"+ 
                                Environment.NewLine+"Producto: "+rg.autoProducto+
                                Environment.NewLine+"Deposito: "+rg.autoDeposito;
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            rt.Entidad = false;
                            return rt;
                        }

                        if (rg.cantidadUnd > ent.fisica) 
                        {
                            rt.Mensaje = "NO HAY DISPONIBILIDAD PARA " +
                                Environment.NewLine + "Producto: " + ent.productos.nombre;
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            rt.Entidad = false;
                            return rt;
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

        public DtoLib.ResultadoAuto Producto_Movimiento_Ajuste_Insertar(DtoLibInventario.Movimiento.Ajuste.Insertar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "";

                        sql = "update sistema_contadores set a_productos_movimientos=a_productos_movimientos+1, a_productos_movimientos_ajustes=a_productos_movimientos_ajustes+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aMov = cnn.Database.SqlQuery<int>("select a_productos_movimientos from sistema_contadores").FirstOrDefault();
                        var aMovAjuste= cnn.Database.SqlQuery<int>("select a_productos_movimientos_ajustes from sistema_contadores").FirstOrDefault();
                        var autoMov = aMov.ToString().Trim().PadLeft(10, '0');
                        var numDoc = aMovAjuste.ToString().Trim().PadLeft(10, '0');
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var entMov = new productos_movimientos()
                        {
                            auto = autoMov,
                            auto_concepto = ficha.autoConcepto,
                            auto_deposito = ficha.autoDepositoOrigen,
                            auto_destino = ficha.autoDepositoDestino,
                            auto_remision = ficha.autoRemision,
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


                        var sql1 = @"INSERT INTO productos_movimientos_detalle (auto_documento, auto_producto, codigo, nombre, " +
                            "cantidad, cantidad_bono, cantidad_und, categoria, fecha, tipo, estatus_anulado, contenido_empaque, " +
                            "empaque, decimales, auto, costo_und, total, costo_compra, estatus_unidad, signo, existencia, " +
                            "fisica, auto_departamento, auto_grupo, cierre_ftp) " +
                            "VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, " +
                            "{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24})";

                        var _auto = 0;
                        foreach (var det in ficha.detalles)
                        {
                            _auto += 1;
                            var xauto = _auto.ToString().Trim().PadLeft(10, '0');

                            var vk = cnn.Database.ExecuteSqlCommand(sql1, autoMov, det.autoProducto, det.codigoProducto, det.nombreProducto,
                                det.cantidad, det.cantidadBono, det.cantidadUnd, det.categoria, fechaSistema.Date, det.tipo,
                                det.estatusAnulado, det.contEmpaque, det.empaque, det.decimales, xauto, det.costoUnd, det.total,
                                det.costoCompra, det.estatusUnidad, det.signo, 0, 0, det.autoDepartamento, det.autoGrupo, ficha.cierreFtp);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO DETALLE [ " + Environment.NewLine + det.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };


                        //ACTUALIZAR DEPOSITO-ENTRADA MERCANCIA
                        foreach (var dt in ficha.prdDeposito)
                        {
                            var entPrdDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == dt.autoProducto && f.auto_deposito == dt.autoDeposito);
                            if (entPrdDep == null)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ PRODUCTO - DEPOSITO ] NO ENCONTRADO " + Environment.NewLine + "Producto: " + dt.autoProducto + ", Deposito: " + dt.autoDeposito;
                                return result;
                            }
                            entPrdDep.fisica += dt.cantidadUnd;
                            entPrdDep.disponible = entPrdDep.fisica;
                            cnn.SaveChanges();
                        };


                        //KARDEX MOV=> ITEMS
                        var sql2 = @"INSERT INTO productos_kardex (auto_producto,total,auto_deposito,auto_concepto,auto_documento,
                            fecha,hora,documento,modulo,entidad,signo,cantidad,cantidad_bono,cantidad_und,costo_und,estatus_anulado,
                            nota,precio_und,codigo,siglas,codigo_sucursal, cierre_ftp, codigo_deposito, nombre_deposito, 
                            codigo_concepto, nombre_concepto) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, 
                            {12}, {13}, {14}, {15},{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25})";
                        foreach (var dt in ficha.movKardex)
                        {
                            var vk = cnn.Database.ExecuteSqlCommand(sql2, dt.autoProducto, dt.total, dt.autoDeposito,
                                dt.autoConcepto, autoMov, fechaSistema.Date, fechaSistema.ToShortTimeString(), numDoc,
                                dt.modulo, dt.entidad, dt.signoMov, dt.cantidad, dt.cantidadBono, dt.cantidadUnd, dt.costoUnd, dt.estatusAnulado,
                                dt.nota, dt.precioUnd, dt.codigoMov, dt.siglasMov, dt.codigoSucursal, ficha.cierreFtp, dt.codigoDeposito,
                                dt.nombreDeposito, dt.codigoConcepto, dt.nombreConcepto);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO KARDEX [ " + Environment.NewLine + dt.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cnn.SaveChanges();
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
                foreach (var eve in e.Entries)
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
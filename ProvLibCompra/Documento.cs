using LibEntityCompra;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibCompra
{
    
    public partial class Provider: ILibCompras.IProvider
    {

        public DtoLib.ResultadoAuto Compra_DocumentoAgregarFactura(DtoLibCompra.Documento.Cargar.Factura.Ficha docFac)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "";

                        sql = "update sistema_contadores set a_compras=a_compras+1, a_cxp=a_cxp+1, a_sistema_transito_asiento=a_sistema_transito_asiento+1 ";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aMovCompra = cnn.Database.SqlQuery<int>("select a_compras from sistema_contadores").FirstOrDefault();
                        var aMovAsiento = cnn.Database.SqlQuery<int>("select a_sistema_transito_asiento from sistema_contadores").FirstOrDefault();
                        var aMovCxP = cnn.Database.SqlQuery<int>("select a_cxp from sistema_contadores").FirstOrDefault();
                        var autoMovCompra = aMovCompra.ToString().Trim().PadLeft(10, '0');
                        var autoMovAsiento = aMovAsiento.ToString().Trim().PadLeft(10, '0');
                        var autoMovCxP = aMovCxP.ToString().Trim().PadLeft(10, '0');
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var doc=docFac.documento;
                        var entMovCompra = new compras()
                        {
                            auto = autoMovCompra,
                            documento = doc.documentoNro,
                            fecha = doc.fechaDocumento,
                            fecha_vencimiento = doc.fechaVencimiento,
                            razon_social = doc.nombreRazonSocialProveedor,
                            dir_fiscal = doc.direccionFiscalProveedor,
                            ci_rif = doc.ciRifProveedor,
                            tipo = doc.tipoDocumento,
                            exento = doc.montoExento,
                            base1 = doc.montoBase1,
                            base2 = doc.montoBase2,
                            base3 = doc.montoBase3,
                            impuesto1 = doc.montoImpuesto1,
                            impuesto2 = doc.montoImpuesto2,
                            impuesto3 = doc.montoImpuesto3,
                            @base = doc.montoBase,
                            impuesto = doc.montoImpuesto,
                            total = doc.montoTotal,
                            tasa1 = doc.valorTasaIva1,
                            tasa2 = doc.valorTasaIva2,
                            tasa3 = doc.valorTasaIva3,
                            nota = doc.notaDocumento,
                            tasa_retencion_iva = doc.valorTasaRetencionIva,
                            tasa_retencion_islr = doc.valorTasaRetencionISLR,
                            retencion_iva = doc.montoRetencionIva,
                            retencion_islr = doc.montoRetencionISLR,
                            auto_proveedor = doc.autoProveedor,
                            codigo_proveedor = doc.codigoProveedor,
                            mes_relacion = doc.mesRelacion,
                            control = doc.controlNro,
                            fecha_registro = fechaSistema.Date,
                            orden_compra = doc.ordenCompraNro,
                            dias = doc.diasCredito,
                            descuento1 = doc.montoDescuento1,
                            descuento2 = doc.montoDescuento2,
                            cargos = doc.montoCargo,
                            descuento1p = doc.valorPorcDescuento1,
                            descuento2p = doc.valorPorcDescuento2,
                            cargosp = doc.valorPorccargo,
                            columna = doc.columna,
                            estatus_anulado = doc.esAnulado,
                            aplica = doc.aplicaDocumentoNro,
                            comprobante_retencion = doc.comprobanteRetencionNro,
                            subtotal_neto = doc.subTotalNeto,
                            telefono = doc.telefonoPropveedor,
                            factor_cambio = doc.factorCambio,
                            condicion_pago = doc.codicionPago,
                            usuario = doc.usuarioNombre,
                            codigo_usuario = doc.usuarioCodigo,
                            codigo_sucursal = doc.sucursalCodigo,
                            hora = fechaSistema.ToShortTimeString(),
                            monto_divisa = doc.montoDivisa,
                            estacion = doc.estacionEquipo,
                            renglones = doc.cntRenglones,
                            saldo_pendiente = doc.montoSaldoPendeiente,
                            ano_relacion = doc.anoRelacion,
                            comprobante_retencion_islr = doc.comprobanteRetencionISLR,
                            dias_validez = doc.diasValidez,
                            auto_usuario = doc.usuarioAuto,
                            situacion = doc.situacionDocumento,
                            signo = doc.signoDocumento,
                            serie = doc.serieDocumento,
                            tarifa = doc.tarifa,
                            tipo_remision = doc.tipoRemision,
                            documento_remision = doc.documentoRemision,
                            auto_remision = doc.autoRemision,
                            documento_nombre = doc.documentoNombre,
                            subtotal_impuesto = doc.subTotalImpuesto,
                            subtotal = doc.subTotal,
                            auto_cxp = autoMovCxP,
                            tipo_proveedor = doc.tipoProveedor,
                            planilla = doc.planilla,
                            expediente = doc.expediente,
                            anticipo_iva = doc.anticipoIva,
                            terceros_iva = doc.tercerosIva,
                            neto = doc.montoNeto,
                            costo = doc.montoCosto,
                            utilidad = doc.montoUtilidad,
                            utilidadp = doc.valorPorctUtilidad,
                            documento_tipo = doc.documentoTipo,
                            denominacion_fiscal = doc.denominacionFiscal,
                            auto_concepto = doc.autoConcepto,
                            fecha_retencion = doc.fechaRetencion,
                            estatus_cierre_contable = doc.estatusCierreContable,
                            cierre_ftp = doc.cierreFtp,
                        };
                        cnn.compras.Add(entMovCompra);
                        cnn.SaveChanges();
                        
                        var doc2=docFac.cxp;
                        var entMovCxP = new cxp()
                        {
                            auto = autoMovCxP,
                            fecha = fechaSistema.Date,
                            tipo_documento = doc2.tipoDocumento,
                            documento = doc2.documentoNro,
                            fecha_vencimiento = doc2.fechaVencimiento,
                            nota = doc2.nota,
                            importe = doc2.importe,
                            acumulado = doc2.acumulado,
                            auto_proveedor = doc2.autoProveedor,
                            proveedor = doc2.nombreRazonSocialProveedor,
                            ci_rif= doc2.ciRifProveedor,
                            codigo_proveedor=doc2.codigoProveedor,
                            estatus_cancelado=doc2.esCancelado,
                            resta=doc2.resta,
                            estatus_anulado=doc2.esAnulado,
                            auto_documento=autoMovCompra,
                            numero=doc2.numero,
                            auto_agencia=doc2.autoAgencia,
                            agencia=doc2.nombreAgencia,
                            signo=doc2.signoDocumento,
                            dias=doc2.diasCredito,
                            auto_asiento=autoMovAsiento,
                            anexo=doc2.Anexo,
                            estatus_cierre_contable=doc2.estatusCierreContable,
                            importeDivisa=doc2.importeDivisa,
                        };
                        cnn.cxp.Add(entMovCxP);
                        cnn.SaveChanges();


                        var entProveedor = cnn.proveedores.Find(docFac.documento.autoProveedor);
                        if (entProveedor == null) 
                        {
                            result.Mensaje = "[ ID ] PROVEEDOR NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        entProveedor.saldo += docFac.documento.montoTotal;
                        entProveedor.fecha_ult_compra = docFac.documento.fechaDocumento;
                        cnn.SaveChanges();


                        var sqlDetalle = "INSERT INTO compras_detalle (" +
                            "auto_documento, auto_producto, codigo, nombre, auto_departamento, auto_grupo, auto_subgrupo, " +
                            "auto_deposito, cantidad, empaque, descuento1p, descuento2p, descuento3p, descuento1, descuento2, " +
                            "descuento3, total_neto, tasa, impuesto, total, auto, estatus_anulado, fecha, tipo, deposito, " +
                            "signo, auto_proveedor, decimales, contenido_empaque, cantidad_und, costo_und, codigo_deposito, " +
                            "detalle, auto_tasa, categoria, costo_promedio_und, costo_compra, codigo_proveedor, cantidad_bono, " +
                            "costo_bruto, estatus_unidad, fecha_lote, cierre_ftp) " +
                            "VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, " +
                            "{7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, " +
                            "{15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, " +
                            "{25}, {26}, {27}, {28}, {29}, {30}, {31}, " +
                            "{32}, {33}, {34}, {35}, {36}, {37}, {38}, " +
                            "{39}, {40}, {41}, {42})";
                        var _auto=0;
                        foreach (var it in docFac.detalles) 
                        {
                            _auto+=1;
                            var xauto = _auto.ToString().Trim().PadLeft(10, '0');

                            var vdet = cnn.Database.ExecuteSqlCommand(sqlDetalle,autoMovCompra, it.autoProducto, it.codigoProducto, it.nombreProducto,it.autoDepartamento,it.autoGrupo, it.autoSubGrupo,
                                it.autoDeposito, it.cantidadFac, it.empaquePrd, it.valorPorcDescto1, it.valorPorcDescto2, it.valorPorcDescto3, it.montoDescto1, it.montoDescto2,
                                it.montoDescto3, it.totalNeto, it.valorTasaIva, it.montoImpuesto, it.montoTotal, xauto, it.esAnulado, fechaSistema.Date, it.tipoDocumento, it.depositoNombre,
                                it.signo, it.autoProveedor, it.decimalesPrd, it.contenidoEmpaque, it.cantidadUnd, it.costoUnd, it.depositoCodigo,
                                it.detalle, it.autoTasaIva, it.categoriaPrd, it.costoPromedioUnd, it.costoCompra, it.codigoProveedor, it.cantidadBonoFac,
                                it.costoBruto, it.estatusUnidad, it.fechaLote, it.cierreFtp);
                            if (vdet == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR ITEM DETALLE [ " + Environment.NewLine + it.nombreProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cnn.SaveChanges();
                        }

                        var sqlKardex = @"INSERT INTO productos_kardex (auto_producto,total,auto_deposito,auto_concepto,auto_documento,
                            fecha,hora,documento,modulo,entidad,signo,cantidad,cantidad_bono,cantidad_und,costo_und,estatus_anulado,
                            nota,precio_und,codigo,siglas,codigo_sucursal, cierre_ftp, codigo_deposito, nombre_deposito, 
                            codigo_concepto, nombre_concepto) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, 
                            {12}, {13}, {14}, {15},{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25})";
                        foreach (var it in docFac.prdKardex)
                        {
                            var vk = cnn.Database.ExecuteSqlCommand(sqlKardex, it.autoPrd, it.montoTotal, it.autoDeposito,
                                it.autoConcepto, autoMovCompra, fechaSistema.Date, fechaSistema.ToShortTimeString(), it.documentoNro,
                                it.modulo, it.entidad, it.signoDocumento, it.cantidadFac, it.cantidadBonoFac, it.cantidadUnd, it.costoUnd, it.esAnulado,
                                it.nota, it.precioUnd, it.codigoMovDoc, it.siglasMovDoc, it.codigoSucursal, it.cierreFtp, it.codigoDeposito,
                                it.nombreDeposito, it.codigoConcepto, it.nombreConcepto);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO KARDEX [ " + Environment.NewLine + it.autoPrd + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cnn.SaveChanges();
                        };

                        foreach (var it in docFac.prdCosto)
                        {
                            var entPrd = cnn.productos.Find(it.autoPrd);
                            if (entPrd == null)
                            {
                                result.Mensaje= "[ ID ] PRODUCTO NO ENCONTRADO";
                                result.Result= DtoLib.Enumerados.EnumResult.isError;
                                return result;;
                            }

                            var cPromedio = 0.0m;
                            var cActual = 0.0m;
                            var cCompra= 0.0m;
                            var ex=0.0m;
                            var exLista = cnn.productos_deposito.Where(w => w.auto_producto == it.autoPrd).ToList();
                            if (exLista.Count>0)
                                ex=exLista.Sum(s => s.fisica);
                            if (ex!=0)
                            {
                                cActual = entPrd.costo_promedio_und * ex;
                                cCompra = it.costoUnd * it.cntUnd;
                                cPromedio = (cActual + cCompra) / (ex+ it.cntUnd);
                            }
                            else 
                            {
                                cActual = 0.0m;
                                cCompra = it.costoUnd * it.cntUnd;
                                cPromedio = (cActual + cCompra) / (it.cntUnd);
                            }

                            entPrd.costo = it.costo;
                            entPrd.costo_und = it.costoUnd;
                            entPrd.costo_proveedor = it.costo;
                            entPrd.costo_proveedor_und = it.costoUnd;
                            entPrd.costo_promedio = cPromedio*it.contenido;
                            entPrd.costo_promedio_und = cPromedio;
                            entPrd.divisa = it.costoDivisa;
                            entPrd.fecha_movimiento = fechaSistema.Date;
                            entPrd.fecha_cambio = fechaSistema.Date;
                            entPrd.fecha_ult_costo = fechaSistema.Date;
                            cnn.SaveChanges();
                        }

                        foreach (var it in docFac.prdCostosHistorico)
                        {
                            var entHist = new productos_costos()
                            {
                                auto_producto = it.autoPrd,
                                costo = it.costo,
                                costo_divisa = it.costoDivisa ,
                                divisa = it.tasaDivisa,
                                documento = it.documento ,
                                estacion = docFac.documento.estacionEquipo,
                                fecha = fechaSistema.Date,
                                hora = fechaSistema.ToShortTimeString(),
                                nota = it.nota ,
                                serie = it.serie ,
                                usuario = docFac.documento.usuarioNombre,
                            };
                            cnn.productos_costos.Add(entHist);
                            cnn.SaveChanges();
                        }

                        foreach (var it in docFac.prdDeposito)
                        {
                            var entPrdDeposito = cnn.productos_deposito.FirstOrDefault(f => f.auto_deposito == it.autoDep && f.auto_producto == it.autoPrd);
                            if (entPrdDeposito == null) 
                            {
                                result.Mensaje = "[ ID ] PRODUCTO - DEPOSITO NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result; ;
                            }

                            if (entPrdDeposito != null)
                            {
                                entPrdDeposito.fisica += it.cantidadUnd;
                                entPrdDeposito.disponible += it.cantidadUnd;
                                cnn.SaveChanges();
                            }
                        }

                        foreach (var it in docFac.prdProveedor)
                        {
                            var entPrdPrv = new productos_proveedor()
                            {
                                auto_producto = it.autoPrd,
                                auto_proveedor = it.autoProveedor,
                                codigo_producto = it.codigoRefProveedor,
                            };
                            cnn.productos_proveedor.Add(entPrdPrv);
                            cnn.SaveChanges();
                        }

                        foreach (var it in docFac.prdPrecios)
                        {
                            var entPrd = cnn.productos.Find(it.autoPrd);
                            if (entPrd == null) 
                            {
                                result.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result; ;
                            }

                            entPrd.pdf_1 = it.pDivisaFull_1;
                            entPrd.pdf_2 = it.pDivisaFull_2;
                            entPrd.pdf_3 = it.pDivisaFull_3;
                            entPrd.pdf_4 = it.pDivisaFull_4;
                            entPrd.pdf_pto = it.pDivisaFull_5;
                            entPrd.precio_1 = it.precioNeto_1;
                            entPrd.precio_2 = it.precioNeto_2;
                            entPrd.precio_3 = it.precioNeto_3;
                            entPrd.precio_4 = it.precioNeto_4;
                            entPrd.precio_pto = it.precioNeto_5;
                            cnn.SaveChanges();
                        }

                        foreach (var it in docFac.prdPreciosHistorico)
                        {
                            var entHist = new productos_precios()
                            {
                                auto_producto = it.autoPrd,
                                estacion = docFac.documento.estacionEquipo,
                                fecha = fechaSistema.Date,
                                hora = fechaSistema.ToShortTimeString(),
                                usuario = docFac.documento.usuarioNombre,
                                nota = it.nota,
                                precio = it.precio,
                                precio_id = it.precioId,
                            };
                            cnn.productos_precios.Add(entHist);
                            cnn.SaveChanges();
                        }

                        ts.Complete();
                        result.Auto = autoMovCompra;
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

        public DtoLib.ResultadoEntidad<DtoLibCompra.Documento.Visualizar.Ficha> Compra_DocumentoVisualizar(string auto)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Documento.Visualizar.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var ent = cnn.compras.Find(auto);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] DOCUMENTO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var det = cnn.compras_detalle.Where(f => f.auto_documento == auto).ToList();
                    var doc = new DtoLibCompra.Documento.Visualizar.Ficha()
                    {
                        anoRelacion = ent.ano_relacion,
                        cargoPorct = ent.cargosp,
                        controlNro = ent.control,
                        descuentoPorct = ent.descuento1p,
                        diasCredito = ent.dias,
                        documentoNombre = ent.documento_nombre,
                        documentoNro = ent.documento,
                        documentoSerie = ent.serie,
                        equipoEstacion = ent.estacion,
                        factorCambio = ent.factor_cambio,
                        fechaEmision = ent.fecha,
                        fechaRegistro = ent.fecha_registro,
                        horaRegistro=ent.hora,
                        fechaVencimiento = ent.fecha_vencimiento,
                        mesRelacion = ent.mes_relacion,
                        montoBase = ent.@base,
                        montoBase1 = ent.base1,
                        montoBase2 = ent.base2,
                        montoBase3 = ent.base3,
                        montoCargo = ent.cargos,
                        montoDescuento = ent.descuento1,
                        montoDivisa = ent.monto_divisa,
                        montoExento = ent.exento,
                        montoImpuesto = ent.impuesto,
                        montoTotal = ent.total,
                        notas = ent.nota,
                        ordenCompraNro = ent.orden_compra,
                        provCiRif = ent.ci_rif,
                        provCodigo = ent.codigo_proveedor,
                        provDirFiscal = ent.dir_fiscal,
                        provNombre = ent.razon_social,
                        provTelefono = ent.telefono,
                        renglones = ent.renglones,
                        signo = ent.signo,
                        subTotal = ent.subtotal_neto,
                        tasaIva1 = ent.tasa1,
                        tasaIva2 = ent.tasa2,
                        tasaIva3 = ent.tasa3,
                        usuarioCodigo = ent.codigo_usuario,
                        usuarioNombre = ent.usuario,
                        montoIva1 = ent.impuesto1,
                        montoIva2 = ent.impuesto2,
                        montoIva3 = ent.impuesto3,
                    };
                    var lista = det.Select(s =>
                    {
                        var dt = new DtoLibCompra.Documento.Visualizar.FichaDetalle()
                        {
                            cntFactura = s.cantidad,
                            contenido = s.contenido_empaque,
                            depositoCodigo = s.codigo_deposito,
                            depositoNombre = s.deposito,
                            dscto1p = s.descuento1p,
                            dscto2p = s.descuento2p,
                            dscto3p = s.descuento3p,
                            dscto1m = s.descuento1,
                            dscto2m = s.descuento2,
                            dscto3m = s.descuento3,
                            importe = s.total_neto,
                            empaqueCompra = s.empaque,
                            prdCodigo = s.codigo,
                            prdNombre = s.nombre,
                            precioFactura = s.costo_bruto,
                            tasaIva = s.tasa,
                        };
                        return dt;
                    }).ToList();
                    doc.detalles = lista;

                    result.Entidad = doc;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoLista<DtoLibCompra.Documento.Lista.Resumen> Compra_DocumentoGetLista(DtoLibCompra.Documento.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibCompra.Documento.Lista.Resumen>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = "SELECT " +
                        "auto, fecha as fechaEmision, tipo, documento, " +
                        "documento_nombre as tipoDocNombre, fecha_registro as fechaRegistro, " +
                        "codigo_sucursal as codigoSuc, razon_social as provNombre, ci_rif as provCiRif, " +
                        "total as monto, situacion, monto_Divisa as montoDivisa, estatus_anulado as estatusAnulado ";

                    var sql_2 = " FROM compras ";

                    var sql_3 = " where 1=1 ";

                    if (filtro.segun_FechaEmisionDesde.HasValue)
                    {
                        p1.ParameterName = "@fDesde";
                        p1.Value = filtro.segun_FechaEmisionDesde;
                        sql_3 += " and fecha>=@fDesde ";
                    }
                    if (filtro.segun_FechaEmisionHasta.HasValue)
                    {
                        p2.ParameterName = "@fHasta";
                        p2.Value = filtro.segun_FechaEmisionHasta;
                        sql_3 += " and fecha<=@fHasta ";
                    }

                    var sql = sql_1 + sql_2 + sql_3;
                    var lst = cnn.Database.SqlQuery<DtoLibCompra.Documento.Lista.Resumen>(sql, p1, p2, p3).ToList();
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

    }

}
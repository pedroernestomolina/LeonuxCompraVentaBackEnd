﻿using LibEntityPos;
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

        public DtoLib.ResultadoAuto Documento_Agregar_Factura(DtoLibPos.Documento.Agregar.Factura.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var mesRelacion = fechaSistema.Month.ToString().Trim().PadLeft(2, '0');
                        var anoRelacion = fechaSistema.Year.ToString().Trim().PadLeft(4, '0');
                        var fechaNula= new DateTime(2000,1,1);

                        var sql = "update sistema_contadores set a_ventas=a_ventas+1, a_cxc=a_cxc+1, a_cxc_recibo=a_cxc_recibo+1, a_cxc_recibo_numero=a_cxc_recibo_numero+1";
                        var r1 = cn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError ;
                            return result;
                        }

                        var aVenta = cn.Database.SqlQuery<int>("select a_ventas from sistema_contadores").FirstOrDefault();
                        var aCxC = cn.Database.SqlQuery<int>("select a_cxc from sistema_contadores").FirstOrDefault();
                        var aCxCRecibo = cn.Database.SqlQuery<int>("select a_cxc_recibo from sistema_contadores").FirstOrDefault();
                        var aCxCReciboNumero = cn.Database.SqlQuery<int>("select a_cxc_recibo_numero from sistema_contadores").FirstOrDefault();

                        var autoVenta = aVenta.ToString().Trim().PadLeft(10, '0');
                        var autoCxC = aCxC.ToString().Trim().PadLeft(10, '0');
                        var autoRecibo = aCxCRecibo.ToString().Trim().PadLeft(10, '0');
                        var reciboNUmero = aCxCReciboNumero.ToString().Trim().PadLeft(10, '0');
                        var fechaVenc = fechaSistema.AddDays(ficha.Dias);

                        //DOCUMENTO VENTA
                        var entVenta = new ventas()
                        {
                            auto = autoVenta,
                            documento = ficha.DocumentoNro,
                            fecha = fechaSistema.Date,
                            fecha_vencimiento = fechaVenc.Date,
                            razon_social = ficha.RazonSocial,
                            dir_fiscal = ficha.DirFiscal,
                            ci_rif = ficha.CiRif,
                            tipo = ficha.Tipo,
                            exento = ficha.Exento,
                            base1 = ficha.Base1,
                            base2 = ficha.Base2,
                            base3 = ficha.Base3,
                            impuesto1 = ficha.Impuesto1,
                            impuesto2 = ficha.Impuesto2,
                            impuesto3 = ficha.Impuesto3,
                            @base = ficha.MBase,
                            impuesto = ficha.Impuesto,
                            total = ficha.Total,
                            tasa1 = ficha.Tasa1,
                            tasa2 = ficha.Tasa2,
                            tasa3 = ficha.Tasa3,
                            nota = ficha.Nota,
                            tasa_retencion_iva = ficha.TasaRetencionIva,
                            tasa_retencion_islr = ficha.TasaRetencionIslr,
                            retencion_iva = ficha.RetencionIva,
                            retencion_islr = ficha.TasaRetencionIslr,
                            auto_cliente = ficha.AutoCliente,
                            codigo_cliente = ficha.CodigoCliente,
                            mes_relacion = mesRelacion,
                            control = ficha.Control,
                            fecha_registro = fechaSistema.Date,
                            orden_compra = ficha.OrdenCompra,
                            dias = ficha.Dias,
                            descuento1 = ficha.Descuento1,
                            descuento2 = ficha.Descuento2,
                            cargos = ficha.Cargos,
                            descuento1p = ficha.Descuento1p,
                            descuento2p = ficha.Descuento2p,
                            cargosp = ficha.Cargosp,
                            columna = ficha.Columna,
                            estatus_anulado = ficha.EstatusAnulado,
                            aplica = ficha.Aplica,
                            comprobante_retencion = ficha.ComprobanteRetencion,
                            subtotal_neto = ficha.SubTotalNeto,
                            telefono = ficha.Telefono,
                            factor_cambio = ficha.FactorCambio,
                            codigo_vendedor = ficha.CodigoVendedor,
                            vendedor = ficha.Vendedor,
                            auto_vendedor = ficha.AutoVendedor,
                            fecha_pedido = ficha.FechaPedido,
                            pedido = ficha.Pedido,
                            condicion_pago = ficha.CondicionPago,
                            usuario = ficha.Usuario,
                            codigo_usuario = ficha.CodigoUsuario,
                            codigo_sucursal = ficha.CodigoSucursal,
                            hora = fechaSistema.ToShortTimeString(),
                            transporte = ficha.Transporte,
                            codigo_transporte = ficha.CodigoTransporte,
                            monto_divisa = ficha.MontoDivisa,
                            despachado = ficha.Despachado,
                            dir_despacho = ficha.DirDespacho,
                            estacion = ficha.Estacion,
                            auto_recibo = autoRecibo,
                            recibo = reciboNUmero,
                            renglones = ficha.Renglones,
                            saldo_pendiente = ficha.SaldoPendiente,
                            ano_relacion = anoRelacion,
                            comprobante_retencion_islr = ficha.ComprobanteRetencionIslr,
                            dias_validez = ficha.DiasValidez,
                            auto_usuario = ficha.AutoUsuario,
                            auto_transporte = ficha.AutoTransporte,
                            situacion = ficha.Situacion,
                            signo = ficha.Signo,
                            serie = ficha.Serie,
                            tarifa = ficha.Tarifa,
                            tipo_remision = ficha.TipoRemision,
                            documento_remision = ficha.DocumentoRemision,
                            auto_remision = ficha.AutoRemision,
                            documento_nombre = ficha.DocumentoNombre,
                            subtotal_impuesto = ficha.SubTotalImpuesto,
                            subtotal = ficha.SubTotal,
                            auto_cxc = autoCxC,
                            tipo_cliente = ficha.TipoCliente,
                            planilla = ficha.Planilla,
                            expediente = ficha.Expendiente,
                            anticipo_iva = ficha.AnticipoIva,
                            terceros_iva = ficha.TercerosIva,
                            neto = ficha.Neto,
                            costo = ficha.Costo,
                            utilidad = ficha.Utilidad,
                            utilidadp = ficha.Utilidadp,
                            documento_tipo = ficha.DocumentoTipo,
                            ci_titular = ficha.CiTitular,
                            nombre_titular = ficha.NombreTitular,
                            ci_beneficiario = ficha.CiBeneficiario,
                            nombre_beneficiario = ficha.NombreBeneficiario,
                            clave = ficha.Clave,
                            denominacion_fiscal = ficha.DenominacionFiscal,
                            cambio = ficha.Cambio,
                            estatus_validado = ficha.EstatusValidado,
                            cierre = ficha.Cierre,
                            fecha_retencion = fechaNula,
                            estatus_cierre_contable = ficha.EstatusCierreContable,
                            cierre_ftp = ficha.CierreFtp,
                        };
                        cn.ventas.Add(entVenta);
                        cn.SaveChanges();

                        //DOCUMENTO CXC
                        var _cxc = ficha.DocCxC;
                        var entCxC = new cxc()
                        {
                            auto = autoCxC,
                            c_cobranza = _cxc.CCobranza,
                            c_cobranzap = _cxc.CCobranzap,
                            fecha = fechaSistema.Date,
                            tipo_documento = _cxc.TipoDocumento,
                            documento = _cxc.Documento,
                            fecha_vencimiento = fechaVenc,
                            nota = _cxc.Nota,
                            importe = _cxc.Importe,
                            acumulado = _cxc.Acumulado,
                            auto_cliente = _cxc.AutoCliente,
                            cliente = _cxc.Cliente,
                            ci_rif = _cxc.CiRif,
                            codigo_cliente = _cxc.CodigoCliente,
                            estatus_cancelado = _cxc.EstatusCancelado,
                            resta = _cxc.Resta,
                            estatus_anulado = _cxc.EstatusAnulado,
                            auto_documento = autoVenta,
                            numero = _cxc.Numero,
                            auto_agencia = _cxc.AutoAgencia,
                            agencia = _cxc.Agencia,
                            signo = _cxc.Signo,
                            auto_vendedor = _cxc.AutoVendedor,
                            c_departamento = _cxc.CDepartamento,
                            c_ventas = _cxc.CVentas,
                            c_ventasp = _cxc.CVentasp,
                            serie = _cxc.Serie,
                            importe_neto = _cxc.Importe,
                            dias = _cxc.Dias,
                            castigop = _cxc.CastigoP,
                            cierre_ftp = _cxc.CierreFtp,
                        };
                        cn.cxc.Add(entCxC);
                        cn.SaveChanges();


                        sql = "update sistema_contadores set a_cxc=a_cxc+1";
                        var r2 = cn.Database.ExecuteSqlCommand(sql);
                        if (r2 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES [CXC PAGO]";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aCxCPago = cn.Database.SqlQuery<int>("select a_cxc from sistema_contadores").FirstOrDefault();
                        var autoCxCPago = aCxCPago.ToString().Trim().PadLeft(10, '0');
                        var pago = ficha.DocCxCPago.Pago;

                        //DOCUEMNTO CXC PAGO
                        var entCxCPago = new cxc()
                        {
                            auto = autoCxCPago,
                            c_cobranza = pago.CCobranza,
                            c_cobranzap = pago.CCobranzap,
                            fecha = fechaSistema.Date,
                            tipo_documento = pago.TipoDocumento,
                            documento = reciboNUmero,
                            fecha_vencimiento = fechaSistema.Date,
                            nota = pago.Nota,
                            importe = pago.Importe,
                            acumulado = pago.Acumulado,
                            auto_cliente = pago.AutoCliente,
                            cliente = pago.Cliente,
                            ci_rif = pago.CiRif,
                            codigo_cliente = pago.CodigoCliente,
                            estatus_cancelado = pago.EstatusCancelado,
                            resta = pago.Resta,
                            estatus_anulado = pago.EstatusAnulado,
                            auto_documento = autoRecibo,
                            numero = pago.Numero,
                            auto_agencia = pago.AutoAgencia,
                            agencia = pago.Agencia,
                            signo = pago.Signo,
                            auto_vendedor = pago.AutoVendedor,
                            c_departamento = pago.CDepartamento,
                            c_ventas = pago.CVentas,
                            c_ventasp = pago.CVentasp,
                            serie = pago.Serie,
                            importe_neto = pago.ImporteNeto,
                            dias = pago.Dias,
                            castigop = pago.CastigoP,
                            cierre_ftp = pago.CierreFtp,
                        };
                        cn.cxc.Add(entCxCPago);
                        cn.SaveChanges();

                        //DOCUEMNTO CXC RECIBO
                        var recibo = ficha.DocCxCPago.Recibo;
                        var entCxcRecibo = new cxc_recibos()
                        {
                            auto = autoRecibo,
                            documento = reciboNUmero,
                            fecha = fechaSistema,
                            auto_usuario = recibo.AutoUsuario,
                            importe = recibo.Importe,
                            usuario = recibo.Usuario,
                            monto_recibido = recibo.MontoRecibido,
                            cobrador = recibo.Cobrador,
                            auto_cliente = recibo.AutoCliente,
                            cliente = recibo.Cliente,
                            ci_rif = recibo.CiRif,
                            codigo = recibo.Codigo,
                            estatus_anulado = recibo.EstatusAnulado,
                            direccion = recibo.Direccion,
                            telefono = recibo.Telefono,
                            auto_cobrador = recibo.AutoCobrador,
                            anticipos = recibo.Anticipos,
                            cambio = recibo.Cambio,
                            nota = recibo.Nota,
                            codigo_cobrador = recibo.CodigoCobrador,
                            auto_cxc = autoCxCPago,
                            retenciones = recibo.Retenciones,
                            descuentos = recibo.Descuentos,
                            hora = fechaSistema.ToShortTimeString(),
                            cierre = recibo.Cierre,
                            cierre_ftp = recibo.CierreFtp,
                        };
                        cn.cxc_recibos.Add(entCxcRecibo);
                        cn.SaveChanges();

                        //DOCUMENTO CXC DOCUMENTO
                        var documento = ficha.DocCxCPago.Documento;
                        var sql_InsertarCxCDocumento = @"INSERT INTO cxc_documentos (id  , fecha , tipo_documento , documento , importe , " +
                                    "operacion , auto_cxc , auto_cxc_pago , auto_cxc_recibo , numero_recibo, fecha_recepcion, dias, castigop, comisionp, cierre_ftp) " +
                                    "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14})";
                        var vCxcDoc = cn.Database.ExecuteSqlCommand(sql_InsertarCxCDocumento,
                            documento.Id,
                            fechaSistema.Date,
                            documento.TipoDocumento,
                            documento.Documento,
                            documento.Importe,
                            documento.Operacion,
                            autoCxC,
                            autoCxCPago,
                            autoRecibo,
                            reciboNUmero,
                            fechaSistema.Date,
                            documento.Dias, 
                            documento.CastigoP,
                            documento.ComisionP,
                            documento.CierreFtp);
                        if (vCxcDoc == 0)
                        {
                            result.Mensaje = "PROBLEMA AL REGISTRAR DOCUMENTO CXC";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //DOCUEMNTO CXC METODOS PAGO
                        foreach (var fp in ficha.DocCxCPago.MetodoPago)
                        {
                            var sql_InsertarCxCMedioPago = @"INSERT INTO cxc_medio_pago (auto_recibo , auto_medio_pago , auto_agencia, " +
                                        "medio , codigo , monto_recibido , fecha , estatus_anulado , numero , agencia , auto_usuario, " +
                                        "lote, referencia, auto_cobrador, cierre, fecha_agencia, cierre_fpt) "+
                                        "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16})";
                            var vCxcMedioPago = cn.Database.ExecuteSqlCommand(sql_InsertarCxCMedioPago,
                                autoRecibo,
                                fp.AutoMedioPago,
                                fp.AutoAgencia,
                                fp.Medio,
                                fp.Codigo,
                                fp.MontoRecibido,
                                fechaSistema,
                                fp.EstatusAnulado,
                                fp.Numero,
                                fp.Agencia,
                                ficha.AutoUsuario,
                                fp.Lote,
                                fp.Referencia,
                                fp.AutoCobrador,
                                fp.Cierre,
                                fechaNula,
                                fp.CierreFtp);
                            if (vCxcMedioPago == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR METODO PAGO CXC";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }

                        var sql1 = @"INSERT INTO ventas_detalle (auto_documento, auto_producto, codigo, nombre, auto_departamento,
                                    auto_grupo, auto_subgrupo, auto_deposito, cantidad, empaque, precio_neto, descuento1p, descuento2p,
                                    descuento3p, descuento1, descuento2, descuento3, costo_venta, total_neto, tasa, impuesto, total,
                                    auto, estatus_anulado, fecha, tipo, deposito, signo, precio_final, auto_cliente, decimales, 
                                    contenido_empaque, cantidad_und, precio_und, costo_und, utilidad, utilidadp, precio_item, 
                                    estatus_garantia, estatus_serial, codigo_deposito, dias_garantia, detalle, precio_sugerido,
                                    auto_tasa, estatus_corte, x, y, z, corte, categoria, cobranzap, ventasp, cobranzap_vendedor,
                                    ventasp_vendedor, cobranza, ventas, cobranza_vendedor, ventas_vendedor, costo_promedio_und, 
                                    costo_compra, estatus_checked, tarifa, total_descuento, codigo_vendedor, auto_vendedor, hora, cierre_ftp) 
                                    Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15},
                                    {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31},
                                    {32}, {33}, {34}, {35}, {36}, {37}, {38}, {39}, {40}, {41}, {42}, {43}, {44}, {45}, {46}, {47},
                                    {48}, {49}, {50}, {51}, {52}, {53}, {54}, {55}, {56}, {57}, {58}, {59}, {60}, {61}, {62}, {63},
                                    {64}, {65}, {66}, {67})";
                        //CUERPO DEL DOCUMENTO => ITEMS
                        var item = 0;
                        foreach (var dt in ficha.Detalles)
                        {
                            item += 1;
                            var autoItem = item.ToString().Trim().PadLeft(10, '0');

                            var vd = cn.Database.ExecuteSqlCommand(sql1, autoVenta, dt.AutoProducto, dt.Codigo, dt.Nombre, dt.AutoDepartamento,
                                dt.AutoGrupo, dt.AutoSubGrupo, dt.AutoDeposito, dt.Cantidad, dt.Empaque, dt.PrecioNeto, dt.Descuento1p,
                                dt.Descuento2p, dt.Descuento3p, dt.Descuento1, dt.Descuento2, dt.Descuento3,
                                dt.CostoVenta, dt.TotalNeto, dt.Tasa, dt.Impuesto, dt.Total, autoItem, dt.EstatusAnulado, fechaSistema.Date,
                                dt.Tipo, dt.Deposito, dt.Signo, dt.PrecioFinal, dt.AutoCliente, dt.Decimales, dt.ContenidoEmpaque,
                                dt.CantidadUnd, dt.PrecioUnd, dt.CostoUnd, dt.Utilidad, dt.Utilidadp, dt.PrecioItem, dt.EstatusGarantia,
                                dt.EstatusSerial, dt.CodigoDeposito, dt.DiasGarantia, dt.Detalle, dt.PrecioSugerido, dt.AutoTasa, dt.EstatusCorte,
                                dt.X, dt.Y, dt.Z, dt.Corte, dt.Categoria, dt.Cobranzap, dt.Ventasp, dt.CobranzapVendedor,
                                dt.VentaspVendedor, dt.Cobranza, dt.Ventas, dt.CobranzaVendedor, dt.VentasVendedor,
                                dt.CostoPromedioUnd, dt.CostoCompra, dt.EstatusChecked, dt.Tarifa, dt.TotalDescuento,
                                dt.CodigoVendedor, dt.AutoVendedor, fechaSistema.ToShortTimeString(), dt.CierreFtp);
                            if (vd == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR ITEM [ " + Environment.NewLine + dt.Nombre + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }

                        //DEPOSITO ACTUALIZAR
                        foreach (var dt in ficha.ActDeposito)
                        {
                            var entPrdDeposito = cn.productos_deposito.FirstOrDefault(w =>
                                w.auto_producto == dt.AutoProducto &&
                                w.auto_deposito == dt.AutoDeposito);
                            if (entPrdDeposito == null)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR DEPOSITO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            entPrdDeposito.fisica -= dt.CantUnd;
                            entPrdDeposito.disponible -= dt.CantUnd;
                            cn.SaveChanges();
                        }

                        var sql2 = @"INSERT INTO productos_kardex (auto_producto,total,auto_deposito,auto_concepto,auto_documento,
                                    fecha,hora,documento,modulo,entidad,signo,cantidad,cantidad_bono,cantidad_und,costo_und,estatus_anulado,
                                    nota,precio_und,codigo,siglas, 
                                    codigo_sucursal, cierre_ftp, codigo_deposito, nombre_deposito,
                                    codigo_concepto, nombre_concepto) 
                                    VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, 
                                    {12}, {13}, {14}, {15},{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25})";
                        //KARDEX MOV=> ITEMS
                        foreach (var dt in ficha.MovKardex)
                        {
                            var vk = cn.Database.ExecuteSqlCommand(sql2, dt.AutoProducto, dt.Total, dt.AutoDeposito,
                                dt.AutoConcepto, autoVenta, fechaSistema.Date, fechaSistema.ToShortTimeString(), dt.Documento,
                                dt.Modulo, dt.Entidad, dt.Signo, dt.Cantidad, dt.CantidadBono, dt.CantidadUnd, dt.CostoUnd,
                                dt.EstatusAnulado, dt.Nota, dt.PrecioUnd, dt.Codigo, dt.Siglas, dt.CodigoSucursal, dt.CierreFtp, 
                                dt.CodigoDeposito, dt.NombreDeposito, dt.CodigoConcepto, dt.NombreConcepto);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO KARDEX [ " + Environment.NewLine + dt.AutoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };

                        var res= ficha.Resumen;
                        var entResumen = cn.p_resumen.Find(res.idResumen);
                        if (entResumen == null) 
                        {
                            result.Mensaje = "[ ID ] POS RESUMEN NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        entResumen.m_efectivo += res.mEfectivo;
                        entResumen.cnt_efectivo += res.cntEfectivo;
                        entResumen.m_divisa += res.mDivisa;
                        entResumen.cnt_divisa+= res.cntDivisa;
                        entResumen.m_electronico += res.mElectronico;
                        entResumen.cnt_electronico += res.cntElectronico;
                        entResumen.m_otros+= res.mOtros;
                        entResumen.cnt_otros+= res.cntotros;
                        entResumen.m_devolucion += res.mDevolucion;
                        entResumen.cnt_devolucion += res.cntDevolucion;
                        entResumen.m_contado+= res.mContado;
                        entResumen.m_credito+= res.mCredito;
                        entResumen.cnt_doc += res.cntDoc;
                        entResumen.cnt_fac+= res.cntFac;
                        entResumen.cnt_ncr += res.cntNCr;
                        entResumen.m_fac+= res.mFac;
                        entResumen.m_ncr+= res.mNCr;
                        entResumen.cnt_doc_contado += res.cntDocContado;
                        entResumen.cnt_doc_credito += res.cntDocCredito;

                        ts.Complete();
                        result.Auto = autoVenta;
                    }
                };
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
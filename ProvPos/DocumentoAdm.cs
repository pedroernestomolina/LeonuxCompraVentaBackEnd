using LibEntityPos;
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

        public DtoLib.ResultadoEntidad<DtoLibPos.DocumentoAdm.Anular.CapturarData.Ficha> DocumentoAdm_Anular_CapturarData(string idDoc)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoAuto DocumentoAdm_Agregar_Presupuesto(DtoLibPos.DocumentoAdm.Agregar.Presupuesto.Ficha ficha)
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
                        var fechaNula = new DateTime(2000, 1, 1);

                        var sql = "update sistema_contadores set a_ventas=a_ventas+1, a_ventas_presupuesto=a_ventas_presupuesto+1";
                        var r1 = cn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var aVenta = cn.Database.SqlQuery<int>("select a_ventas from sistema_contadores").FirstOrDefault();
                        var aDocumento = cn.Database.SqlQuery<int>("select a_ventas_presupuesto from sistema_contadores").FirstOrDefault();
                        var largo = 0;
                        largo = 10 - ficha.Prefijo.Length;
                        var fechaVenc = fechaSistema.AddDays(ficha.Dias);
                        var autoVenta = ficha.Prefijo + aVenta.ToString().Trim().PadLeft(largo, '0');
                        var autoCxC = "";
                        var autoRecibo = "";
                        var reciboNUmero = "";
                        var documentoNro = aDocumento.ToString().Trim().PadLeft(10, '0');

                        //DOCUMENTO VENTA
                        var entVenta = new ventas()
                        {
                            auto = autoVenta,
                            documento = documentoNro,
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

                        //DETALLES
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
                       

                        //TEMPORAL VENTA-DETALLE
                        var sql2 = @"DELETE from p_ventaadm_det where id_ventaAdm=@p1";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                        p1.ParameterName = "@p1";
                        p1.Value = ficha.VentaTemporal.id;
                        var vk = cn.Database.ExecuteSqlCommand(sql2, p1);
                        if (vk == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ELIMINAR REGISTRO TEMPORAL-VENTA-DETALLES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //TEMPORAL VENTA
                        var sql3 = @"DELETE from p_ventaadm where id=@p2";
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                        p2.ParameterName = "@p2";
                        p2.Value = ficha.VentaTemporal.id;
                        vk = cn.Database.ExecuteSqlCommand(sql3, p2);
                        if (vk == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ELIMINAR REGISTRO TEMPORAL-VENTA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        
                        cn.SaveChanges();
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

        public DtoLib.Resultado DocumentoAdm_Anular_Presupuesto(DtoLibPos.DocumentoAdm.Anular.Prersupuesto.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var fechaNula = new DateTime(2000, 1, 1);

                        var ent = cn.ventas.Find(ficha.autoDocumento);
                        if (ent == null)
                        {
                            result.Mensaje = "PROBLEMA AL ENCONTRAR DOCUMENTO [ NO REGISTRADO ] ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (ent.estatus_anulado == "1")
                        {
                            result.Mensaje = "PROBLEMA ESTATUS DEL DOCUMENTO [ ANULADO ] ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //AUDITORIA
                        var sql = @"INSERT INTO `auditoria_documentos` (`auto_documento`, `auto_sistema_documentos`, 
                                    `auto_usuario`, `usuario`, `codigo`, `fecha`, `hora`, `memo`, `estacion`, `ip`) 
                                    VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, '')";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.auditoria.autoSistemaDocumento);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@p3", ficha.auditoria.autoUsuario);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@p4", ficha.auditoria.usuario);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@p5", ficha.auditoria.codigo);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@p6", fechaSistema.Date);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@p7", fechaSistema.ToShortTimeString());
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@p8", ficha.auditoria.motivo);
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter("@p9", ficha.auditoria.estacion);
                        var v1 = cn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9);
                        if (v1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO AUDITORIA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //DOCUMENTO
                        sql = "update ventas set estatus_anulado='1' where auto=@p1";
                        var v2 = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v2 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] AL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //ITEMS DETALLE
                        sql = "update ventas_detalle set estatus_anulado='1' where auto_documento=@p1";
                        var v3 = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v3 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] A LOS ITEMS DEL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        cn.SaveChanges();
                        ts.Complete();
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
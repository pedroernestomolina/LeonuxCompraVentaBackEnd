using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibVenta
{

    public partial class Provider : ILibVentas.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibVenta.Venta.Resumen> VentaLista(DtoLibVenta.Venta.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibVenta.Venta.Resumen>();

            try
            {
                using (var cnn = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var q = cnn.ventas.ToList();
                    if (filtro.segun_FechaEmisionDesde.HasValue)
                    {
                        q = q.Where(w => w.fecha >= filtro.segun_FechaEmisionDesde.Value).ToList();
                    }
                    if (filtro.segun_FechaEmisionHasta.HasValue)
                    {
                        q = q.Where(w => w.fecha >= filtro.segun_FechaEmisionHasta.Value).ToList();
                    }

                    var list = new List<DtoLibVenta.Venta.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            result.Lista = q.Select(s =>
                            {
                                var isAnulado = s.estatus_anulado == "1" ? true : false;
                                var isCerradoContable = s.estatus_cierre_contable == "1" ? true : false;
                                var tipoDoc = DtoLibVenta.Venta.Enumerados.enumTipoDocumento.SinDefinir;
                                var isCredito = s.condicion_pago.Trim().ToUpper() == "CREDITO" ? true : false;
                                switch (s.tipo.Trim().ToUpper())
                                {
                                    case "01":
                                        tipoDoc = DtoLibVenta.Venta.Enumerados.enumTipoDocumento.Factura;
                                        break;
                                    case "02":
                                        tipoDoc = DtoLibVenta.Venta.Enumerados.enumTipoDocumento.NotaDebito;
                                        break;
                                    case "03":
                                        tipoDoc = DtoLibVenta.Venta.Enumerados.enumTipoDocumento.NotaCredito;
                                        break;
                                    case "04":
                                        tipoDoc = DtoLibVenta.Venta.Enumerados.enumTipoDocumento.NotaEntrega;
                                        break;
                                    case "05":
                                        tipoDoc = DtoLibVenta.Venta.Enumerados.enumTipoDocumento.Pedido;
                                        break;
                                    case "06":
                                        tipoDoc = DtoLibVenta.Venta.Enumerados.enumTipoDocumento.Presupuesto;
                                        break;
                                }

                                var r = new DtoLibVenta.Venta.Resumen()
                                {
                                    Auto = s.auto,
                                    CodigoSucursal = s.codigo_sucursal,
                                    ControlNro = s.control,
                                    DocumentoNro = s.documento,
                                    EntidadNombre = s.razon_social,
                                    EntidadRif = s.ci_rif,
                                    FechaEmision = s.fecha,
                                    FechaRegistro = s.fecha_registro,
                                    IsAnulado = isAnulado,
                                    IsCredito = isCredito,
                                    TipoDocumento = tipoDoc,
                                    Total = s.total,
                                    Notas = s.nota,
                                    Signo = s.signo,
                                };
                                return r;
                            }).ToList();
                        }
                        else
                        {
                            result.Lista = list;
                        }
                    }
                    else
                    {
                        result.Lista = list;
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoAuto VentaAgregar(DtoLibVenta.Venta.Agregar ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var ctx = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var r1 = ctx.Database.ExecuteSqlCommand("update sistema_contadores set a_ventas=a_ventas+1");
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR CONTADOR DE VENTAS";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var r2 = ctx.Database.ExecuteSqlCommand("update sistema_contadores set a_cxc=a_cxc+1");
                        if (r2 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR CONTADOR DE CXC ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var cntVenta = ctx.Database.SqlQuery<int>("select a_ventas from sistema_contadores").FirstOrDefault();
                        var cntCxC = ctx.Database.SqlQuery<int>("select a_cxc from sistema_contadores").FirstOrDefault();
                        var autoVenta = cntVenta.ToString().Trim().PadLeft(10, '0');
                        var autoCxC = cntCxC.ToString().Trim().PadLeft(10, '0');

                        var autoCxcPago = "";
                        var autoCxcRecibo = "";
                        var autoCxcReciboNumero = "";
                        if (ficha.CondicionPago == DtoLibVenta.Venta.Enumerados.enumCondicionPago.Contado)
                        {
                            var r3 = ctx.Database.ExecuteSqlCommand("update sistema_contadores set a_cxc_recibo=a_cxc_recibo+1");
                            if (r3 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR CONTADOR DE CXC RECIBO ";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            var r4 = ctx.Database.ExecuteSqlCommand("update sistema_contadores set a_cxc_recibo_numero=a_cxc_recibo_numero+1");
                            if (r4 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR CONTADOR DE CXC RECIBO NUMERO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            var r5 = ctx.Database.ExecuteSqlCommand("update sistema_contadores set a_cxc=a_cxc+1");
                            if (r5 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR CONTADOR DE CXC PAGO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            var cntCxcRecibo = ctx.Database.SqlQuery<int>("select a_cxc_recibo from sistema_contadores").FirstOrDefault();
                            var cntCxcReciboNumero = ctx.Database.SqlQuery<int>("select a_cxc_recibo_numero from sistema_contadores").FirstOrDefault();
                            var cntCxcPago = ctx.Database.SqlQuery<int>("select a_cxc from sistema_contadores").FirstOrDefault();
                            autoCxcRecibo = cntCxcRecibo.ToString().Trim().PadLeft(10, '0');
                            autoCxcReciboNumero = cntCxcReciboNumero.ToString().Trim().PadLeft(10, '0');
                            autoCxcPago = cntCxcPago.ToString().Trim().PadLeft(10, '0');
                        }

                        //VALORES A PROCESAR
                        var fechaSistema = ctx.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var documentoVenta = "";
                        var fechaVencimiento = fechaSistema.AddDays(ficha.DiasCredito);

                        var entVenta = new LibEntityVentas.ventas()
                        {
                            auto = autoVenta,
                            documento = documentoVenta,
                            fecha = fechaSistema.Date,
                            fecha_vencimiento = fechaVencimiento,
                            razon_social = ficha.ClienteNombre,
                            dir_fiscal = ficha.ClienteDirFiscal,
                            ci_rif = ficha.ClienteCiRif,
                            tipo = ficha.DocumentoCodigo, 
                            exento = ficha.AgregarTotales.MontoExento,
                            base1 = ficha.AgregarTotales.MontoBase1,
                            base2 = ficha.AgregarTotales.MontoBase2,
                            base3 = ficha.AgregarTotales.MontoBase3,
                            impuesto1 = ficha.AgregarTotales.MontoImp1,
                            impuesto2 = ficha.AgregarTotales.MontoImp2,
                            impuesto3 = ficha.AgregarTotales.MontoImp3,
                            @base = ficha.AgregarTotales.MontoBase,
                            impuesto = ficha.AgregarTotales.MontoImpuesto,
                            total = ficha.AgregarTotales.MontoTotal,
                            tasa1 = ficha.AgregarTotales.Tasa1,
                            tasa2 = ficha.AgregarTotales.Tasa2,
                            tasa3 = ficha.AgregarTotales.Tasa3,
                            nota = ficha.Notas,
                            tasa_retencion_islr = 0.0m,
                            tasa_retencion_iva = 0.0m,
                            retencion_iva = 0.0m,
                            retencion_islr = 0.0m,
                            auto_cliente = ficha.AutoCliente,
                            codigo_cliente = ficha.ClienteCodigo,
                            mes_relacion = ficha.MesRelacion, 
                            control = ficha.SerialFiscal,
                            fecha_registro = fechaSistema.Date,
                            orden_compra = ficha.AgregarEncabezado.OrdenCompra,
                            dias = ficha.DiasCredito,
                            descuento1 = ficha.AgregarTotales.DescuentoMonto_1,
                            descuento2 = ficha.AgregarTotales.DescuentoMonto_2,
                            cargos = ficha.AgregarTotales.CargoMonto,
                            descuento1p = ficha.AgregarTotales.DescuentoPorct_1,
                            descuento2p = ficha.AgregarTotales.DescuentoPorct_2,
                            cargosp = ficha.AgregarTotales.CargoPorct,
                            columna = "1",
                            estatus_anulado = "0",
                            aplica = "",
                            comprobante_retencion = "",
                            subtotal_neto = ficha.AgregarTotales.SubTotalNeto,
                            telefono = ficha.ClienteTelefono,
                            factor_cambio = ficha.FactorCambio,
                            codigo_vendedor = ficha.VendedorCodigo,
                            vendedor = ficha.VendedorNombre,
                            auto_vendedor = ficha.AutoVendedor,
                            fecha_pedido = ficha.AgregarEncabezado.FechaPedido,
                            pedido = ficha.AgregarEncabezado.Pedido,
                            condicion_pago = ficha.CondicionPagoDescripcion, 
                            usuario = ficha.UsuarioNombre,
                            codigo_usuario = ficha.UsuarioCodigo,
                            codigo_sucursal = ficha.CodigoSucursal,
                            hora = fechaSistema.ToShortTimeString(),
                            transporte = ficha.TransporteNombre,
                            codigo_transporte = ficha.TransporteCodigo,
                            monto_divisa = ficha.AgregarTotales.MontoDivisa,
                            despachado = ficha.AgregarEncabezado.DepachadoPor,
                            dir_despacho = ficha.AgregarEncabezado.DireccionDespacho,
                            estacion = ficha.Estacion,
                            auto_recibo = autoCxcRecibo,
                            recibo = autoCxcReciboNumero,
                            renglones = ficha.Renglones,
                            saldo_pendiente = ficha.AgregarTotales.SaldoPendiente,
                            ano_relacion = ficha.AnoRelacion, 
                            comprobante_retencion_islr = "",
                            dias_validez = 0,
                            auto_usuario = ficha.AutoUsuario,
                            auto_transporte = ficha.AutoTransporte,
                            situacion = ficha.DocumentoSituacion, 
                            signo = ficha.DocumentoSigno,
                            serie = ficha.AgregarEncabezado.Serie,
                            tarifa = ficha.ClienteTarifa,
                            tipo_remision = ficha.AgregarEncabezado.TipoRemision,
                            documento_remision = ficha.AgregarEncabezado.DocumentoRemision,
                            auto_remision = ficha.AgregarEncabezado.AutoRemision,
                            documento_nombre = ficha.DocumentoNombre, 
                            subtotal_impuesto = ficha.AgregarTotales.MontoImpuesto,
                            subtotal = ficha.AgregarTotales.SubTotal,
                            auto_cxc = autoCxC,
                            tipo_cliente = "",
                            planilla = "",
                            expediente = "",
                            anticipo_iva = 0.0m,
                            terceros_iva = 0.0m,
                            neto = ficha.AgregarTotales.VentaNeta,
                            costo = ficha.AgregarTotales.CostoVenta,
                            utilidad = ficha.AgregarTotales.Utilidad,
                            utilidadp = ficha.AgregarTotales.UtilidadPorc,
                            documento_tipo = ficha.DocumentoTipo, 
                            ci_titular = "",
                            nombre_titular = "",
                            ci_beneficiario = "",
                            nombre_beneficiario = "",
                            clave = "",
                            denominacion_fiscal = ficha.ClienteDenominacionFiscal,
                            cambio = ficha.CambioDar,
                            estatus_validado = "0",
                            cierre = "",
                            fecha_retencion = new DateTime(2000, 01, 01),
                            estatus_cierre_contable = "0",
                        };
                        ctx.ventas.Add(entVenta);
                        ctx.SaveChanges();

                        var sql = @"INSERT INTO ventas_detalle (auto_documento, auto_producto, codigo, nombre, auto_departamento,
                                    auto_grupo, auto_subgrupo, auto_deposito, cantidad, empaque, precio_neto, descuento1p, descuento2p,
                                    descuento3p, descuento1, descuento2, descuento3, costo_venta, total_neto, tasa, impuesto, total,
                                    auto, estatus_anulado, fecha, tipo, deposito, signo, precio_final, auto_cliente, decimales, 
                                    contenido_empaque, cantidad_und, precio_und, costo_und, utilidad, utilidadp, precio_item, 
                                    estatus_garantia, estatus_serial, codigo_deposito, dias_garantia, detalle, precio_sugerido,
                                    auto_tasa, estatus_corte, x, y, z, corte, categoria, cobranzap, ventasp, cobranzap_vendedor,
                                    ventasp_vendedor, cobranza, ventas, cobranza_vendedor, ventas_vendedor, costo_promedio_und, 
                                    costo_compra, estatus_checked, tarifa, total_descuento, codigo_vendedor, auto_vendedor, hora) 
                                    Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15},
                                    {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31},
                                    {32}, {33}, {34}, {35}, {36}, {37}, {38}, {39}, {40}, {41}, {42}, {43}, {44}, {45}, {46}, {47},
                                    {48}, {49}, {50}, {51}, {52}, {53}, {54}, {55}, {56}, {57}, {58}, {59}, {60}, {61}, {62}, {63},
                                    {64}, {65}, {66})";
                        //CUERPO DEL DOCUMENTO => ITEMS
                        var item=0;
                        foreach (var dt in ficha.AgregarCuerpo)
                        {
                            item += 1;
                            var autoItem = item.ToString().Trim().PadLeft(10, '0');
                            var isGarantia = dt.IsGarantia ? "1" : "0";
                            var isSerial = dt.IsSerial ? "1" : "0";

                            var vd = ctx.Database.ExecuteSqlCommand(sql, autoVenta, dt.AutoProducto, dt.CodigoPrd, dt.NombrePrd, dt.AutoDepartamento,
                                dt.AutoGrupo, dt.AutoSubGrupo, dt.AutoDeposito, dt.Cantidad, dt.Empaque, dt.PrecioNeto, dt.DescuentoPorc_1,
                                dt.DescuentoPorc_2, dt.DescuentoPorc_3, dt.DescuentoMonto_1, dt.DescuentoMonto_2, dt.DescuentoMonto_3,
                                dt.CostoVenta, dt.TotalNeto, dt.TasaIva, dt.MontoImpuesto, dt.MontoTotal, autoItem, "0", fechaSistema.Date,
                                dt.Tipo, dt.DepositoDescripcion, dt.Signo, dt.PrecioFinal, dt.AutoCliente, dt.Decimales, dt.EmpaqueContenido,
                                dt.CantidadUnd, dt.PrecioUnd, dt.CostoUnd, dt.UtilidadMonto, dt.UtilidadPorc, dt.PrecioItem, isGarantia,
                                isSerial, dt.DepositoCodigo, dt.DiasGarantia, dt.Notas, 0.0m, dt.AutoTasaImpuesto, "0", 0.0m, 0.0m, 0.0m, "",
                                dt.Categoria, 0.0m, 0.0m, 0.0m, 0.0m, 0.0m, 0.0m, 0.0m, 0.0m, dt.CostoPromedioUnd, dt.CostoPromedioCompra,
                                "1", dt.TarifaPrecio, dt.MontoDescuento, dt.VendedorCodigo, dt.AutoVendedor, fechaSistema.ToShortTimeString());
                            if (vd == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR ITEM [ "+Environment.NewLine+dt.NombrePrd+" ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }

                        //DEPOSITO ACTUALIZAR
                        foreach (var dt in ficha.AgregarActProductoDeposito)
                        {
                            var entPrdDeposito = ctx.productos_deposito.FirstOrDefault(w=> 
                                w.auto_producto==dt.AutoProducto && 
                                w.auto_deposito==dt.AutoDeposito) ;
                            if (entPrdDeposito == null) 
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR DEPOSITO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            entPrdDeposito.fisica -= dt.TotalUnd;
                            entPrdDeposito.disponible -= dt.TotalUnd;
                            ctx.SaveChanges();
                        }


                        var sql2=@"INSERT INTO productos_kardex (auto_producto,total,auto_deposito,auto_concepto,auto_documento,
                            fecha,hora,documento,modulo,entidad,signo,cantidad,cantidad_bono,cantidad_und,costo_und,estatus_anulado,
                            nota,precio_und,codigo,siglas) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, 
                            {12}, {13}, {14}, {15},{16}, {17}, {18}, {19})";
                        //KARDEX MOV=> ITEMS
                        foreach (var dt in ficha.AgregarKardex)
                        {
                            var vk = ctx.Database.ExecuteSqlCommand(sql2, dt.AutoProducto, dt.TotalCostoVenta, dt.AutoDeposito ,
                                dt.AutoConcepto ,autoVenta,fechaSistema.Date,fechaSistema.ToShortTimeString(),documentoVenta,
                                dt.Modulo ,dt.Entidad,dt.Signo ,dt.Cantidad,0.0m,dt.CantidadUnd,dt.CostoPromedioUnd,"0",
                                dt.Notas, dt.PrecioUnd,dt.Codigo ,dt.Siglas);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO KARDEX [ " + Environment.NewLine + dt.AutoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };


                        //REGISTRA MOVIMIENTO DE CXC
                        var entCxC = new LibEntityVentas.cxc()
                        {
                            auto = autoCxC,
                            c_cobranza = 0.0m,
                            c_cobranzap = 0.0m,
                            fecha = fechaSistema.Date,
                            tipo_documento = ficha.AgregarCxc.DocumentoVentaTipo, // "FAC",
                            documento = documentoVenta,
                            fecha_vencimiento = fechaVencimiento,
                            nota = ficha.AgregarCxc.Notas, //"",
                            importe = ficha.AgregarCxc.MontoImporteTotal, //.AgregarTotales.MontoTotal,
                            acumulado = ficha.AgregarCxc.MontoAcumulado, // acumulado,
                            auto_cliente = ficha.AgregarCxc.AutoCliente, // ficha.AutoCliente,
                            cliente = ficha.AgregarCxc.ClienteNombre, //  ficha.ClienteNombre,
                            ci_rif = ficha.AgregarCxc.ClienteCiRif, // ficha.ClienteCiRif,
                            codigo_cliente = ficha.AgregarCxc.ClienteCodigo, //ficha.ClienteCodigo,
                            estatus_cancelado = ficha.AgregarCxc.IsCancelado?"1":"0", //  estatusCancelado,
                            resta = ficha.AgregarCxc.MontoResta , //resta,
                            estatus_anulado = "0",
                            auto_documento = autoVenta,
                            numero = "",
                            auto_agencia = "0000000001",
                            agencia = "",
                            signo = ficha.AgregarCxc.Signo , // 1,
                            auto_vendedor = ficha.AgregarCxc.AutoVendedor , // ficha.AutoVendedor,
                            c_departamento = 0.0m,
                            c_ventas = 0.0m,
                            c_ventasp = 0.0m,
                            serie = ficha.AgregarCxc.DocumentoVentaSerie, //  ficha.AgregarEncabezado.Serie,
                            importe_neto = ficha.AgregarCxc.MontoImporteNeto, //  ficha.AgregarTotales.SubTotalNeto,
                            dias = 0,
                            castigop = 0.0m,
                        };
                        ctx.cxc.Add(entCxC);
                        ctx.SaveChanges();


                        //SI LA FORMA DE PAGO ES CONTADO, GENERA MOVIMIENTO DE PAGO
                        if (ficha.CondicionPago == DtoLibVenta.Venta.Enumerados.enumCondicionPago.Contado)
                        {
                            var entCliente = ctx.clientes.Find(ficha.AutoCliente);
                            if (entCliente == null)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR ENTIDAD [ CLIENTE ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            entCliente.fecha_ult_venta = fechaSistema;
                            ctx.SaveChanges();


                            var entCxcPago = new LibEntityVentas.cxc()
                            {
                                auto = autoCxcPago,
                                c_cobranza = 0.0m,
                                c_cobranzap = 0.0m,
                                fecha = fechaSistema.Date,
                                tipo_documento = "PAG",  //EL PAGO HACE REFENCIA AL RECIBO GENERADO DE CXC
                                documento = autoCxcReciboNumero,  //EL PAGO HACE REFENCIA AL RECIBO GENERADO DE CXC
                                fecha_vencimiento = fechaVencimiento,
                                nota = ficha.Notas,
                                importe = ficha.AgregarTotales.MontoTotal,
                                acumulado = 0,
                                auto_cliente = ficha.AutoCliente,
                                cliente = ficha.ClienteNombre,
                                ci_rif = ficha.ClienteCiRif,
                                codigo_cliente = ficha.ClienteCodigo,
                                estatus_cancelado = "0",
                                resta = 0.0m,
                                estatus_anulado = "0",
                                auto_documento = autoCxcRecibo,  //EL PAGO HACE REFENCIA AL RECIBO GENERADO DE CXC
                                numero = "",
                                auto_agencia = "0000000001",
                                agencia = "",
                                signo = -1,
                                auto_vendedor = ficha.AutoVendedor,
                                c_departamento = 0.0m,
                                c_ventas = 0.0m,
                                c_ventasp = 0.0m,
                                serie = "",
                                importe_neto = 0.0m,
                                dias = 0,
                                castigop = 0.0m,
                            };
                            ctx.cxc.Add(entCxcPago);
                            ctx.SaveChanges();

                            var entCxcRecibo = new LibEntityVentas.cxc_recibos()
                            {
                                auto = autoCxcRecibo,
                                documento = autoCxcReciboNumero,
                                fecha = fechaSistema,
                                auto_usuario = ficha.AutoUsuario,
                                importe = ficha.AgregarTotales.MontoTotal,
                                usuario = ficha.UsuarioNombre,
                                monto_recibido = ficha.MontoRecibido,
                                cobrador = ficha.CobradorNombre,
                                auto_cliente = ficha.AutoCliente,
                                cliente = ficha.ClienteNombre,
                                ci_rif = ficha.ClienteCiRif,
                                codigo = ficha.ClienteCodigo,
                                estatus_anulado = "0",
                                direccion = ficha.ClienteDirFiscal,
                                telefono = ficha.ClienteTelefono,
                                auto_cobrador = ficha.AutoCobrador,
                                anticipos = 0.0m,
                                cambio = ficha.CambioDar,
                                nota = ficha.Notas,
                                codigo_cobrador = ficha.CobradorCodigo,
                                auto_cxc = autoCxC,
                                retenciones = 0.0m,
                                descuentos = 0.0m,
                                hora = "",
                                cierre = "",
                            };
                            ctx.cxc_recibos.Add(entCxcRecibo);
                            ctx.SaveChanges();

                            var entCxcDocumento = new LibEntityVentas.cxc_documentos()
                            {
                                id = 1,
                                fecha = fechaSistema.Date,
                                tipo_documento = "FAC",
                                documento = documentoVenta,
                                importe = ficha.AgregarTotales.MontoTotal,
                                operacion = "Pago",
                                auto_cxc = autoCxC,
                                auto_cxc_pago = autoCxcPago,
                                auto_cxc_recibo = autoCxcRecibo,
                                numero_recibo = autoCxcReciboNumero,
                                fecha_recepcion = new DateTime(2000, 01, 01),
                                dias = 0,
                                castigop = 0.0m,
                                comisionp = 0.0m,
                            };
                            ctx.cxc_documentos.Add(entCxcDocumento);
                            ctx.SaveChanges();

                            foreach (var fp in ficha.AgregarFormaPago)
                            {
                                var entCxcMedioPago = new LibEntityVentas.cxc_medio_pago()
                                {
                                    auto_recibo = autoCxcRecibo,
                                    auto_medio_pago = fp.AutoMedioPago,
                                    auto_agencia = fp.AutoAgencia,
                                    medio = fp.MedioPagoDescripcion,
                                    codigo = fp.MedioPagoCodigo,
                                    monto_recibido = fp.MontoRecibido,
                                    fecha = fechaSistema,
                                    estatus_anulado = "0",
                                    numero = fp.NumeroRef,
                                    agencia = fp.AgenciaDescripcion,
                                    auto_usuario = ficha.AutoUsuario,
                                    lote = fp.Lote,
                                    referencia = fp.Referencia,
                                    auto_cobrador = ficha.AutoCobrador,
                                    cierre = "",
                                    fecha_agencia = new DateTime(2001, 01, 01),
                                };
                                ctx.cxc_medio_pago.Add(entCxcMedioPago);
                                ctx.SaveChanges();
                            }
                        }
                        else 
                        {
                            var entCliente = ctx.clientes.Find(ficha.AutoCliente);
                            if (entCliente == null)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR ENTIDAD [ CLIENTE ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            entCliente.fecha_ult_venta = fechaSistema;
                            entCliente.saldo += ficha.AgregarTotales.MontoTotal;
                            entCliente.disponible -= ficha.AgregarTotales.MontoTotal;
                            ctx.SaveChanges();
                        }
                        
                        ts.Complete();
                        result.Auto = autoVenta;
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
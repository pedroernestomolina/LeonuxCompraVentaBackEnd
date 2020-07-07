using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{

    public partial class Provider : IPosOffLine.IProvider
    {

        public DtoLib.ResultadoId VentaDocumento_Agregar(DtoLibPosOffLine.VentaDocumento.Agregar ficha)
        {
            var result = new DtoLib.ResultadoId();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    using (var ts = cnn.Database.BeginTransaction())
                    {
                        var fechaSistema = DateTime.Now.Date; //cnn.Database.SqlQuery<DateTime>("select date('now')").FirstOrDefault();
                        var mesRelacion = fechaSistema.Month.ToString().Trim().PadLeft(2, '0');
                        var anoRelacion = fechaSistema.Year.ToString();

                        var entSerie = cnn.Serie.FirstOrDefault(f => f.serie1.Trim().ToUpper() == ficha.Serie.Trim().ToUpper());
                        if (entSerie == null) 
                        {
                            result.Mensaje = "SERIE DOCUMENTO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        entSerie.correlativo += 1;
                        cnn.SaveChanges();
                        var _documento = entSerie.correlativo.ToString().Trim().PadLeft(10, '0');


                        var entVenta = new LibEntitySqLitePosOffLine.Venta()
                        {
                            idJornada=ficha.IdJornada,
                            idOperador=ficha.IdOperador,
                            documento = _documento,
                            fecha = fechaSistema.ToShortDateString(),
                            idCliente=ficha.ClienteId,
                            nombreRazonSocial = ficha.ClienteNombreRazonSocial,
                            dirFiscal = ficha.ClienteDirFiscal,
                            ciRif = ficha.ClienteCiRif,
                            montoExento = ficha.MontoExento,
                            montoBase = ficha.MontoBase,
                            montoImpuesto = ficha.MontoImpuesto,
                            base1 = ficha.MontoBase_1,
                            base2 = ficha.MontoBase_2,
                            base3 = ficha.MontoBase_3,
                            impuesto1 = ficha.MontoIva_1,
                            impuesto2 = ficha.MontoIva_2,
                            impuesto3 = ficha.MontoIva_3,
                            tasaIva1 = ficha.TasaIva_1,
                            tasaIva2 = ficha.TasaIva_2,
                            tasaIva3 = ficha.TasaIva_3,
                            mesRelacion = mesRelacion,
                            control = ficha.Control,
                            descuentoMonto1 = ficha.MontoDescuento_1,
                            descuentoMonto2 = ficha.MontoDescuento_2,
                            cargoMonto1 = ficha.MontoCargo_1,
                            descuentoPorc1 = ficha.PorcDescuento_1,
                            descuentoPorc2 = ficha.PorcDescuento_2,
                            cargoPorc_1 = ficha.PorcCargo_1,
                            estatusActivo = 1,
                            tipoDocumento = ficha.TipoDocumento,
                            aplica = ficha.Aplica,
                            montoSubTotalNeto = ficha.MontoSubTotalNeto,
                            telefono = ficha.ClienteTelefono,
                            factorCambio = ficha.FactorCambio,
                            usuario = ficha.UsuarioDescripcion,
                            usuarioCodigo = ficha.UsuarioCodigo,
                            hora = ficha.HoraEmision,
                            montoDivisa = ficha.MontoDivisa,
                            estacion = ficha.Estacion,
                            renglones = ficha.Renglones,
                            anoRelacion = anoRelacion,
                            autoUsuario = ficha.AutoUsuario,
                            signo = ficha.SignoDocumento,
                            serie = ficha.Serie,
                            montoSubTotalImpuesto = ficha.MontoSubTotalImpuesto,
                            montoSubTotal = ficha.MontoSubTotal,
                            montoVentaNeta = ficha.MontoVentaNeta,
                            montoCostoVenta = ficha.MontoCostoVenta,
                            montoUtilidad = ficha.MontoUtilidad,
                            montoUtilidadPorc = ficha.PorcUtilidad,
                            montoTotal = ficha.MontoTotal,
                            codigoSucursal = ficha.CodigoSucursal,
                            prefijo=ficha.Prefijo,
                            autoDeposito = ficha.AutoDeposito,
                            codigoDeposito = ficha.CodigoDeposito,
                            deposito = ficha.DescripcionDeposito,
                            autoVendedor = ficha.AutoVendedor,
                            codigoVendedor = ficha.CodigoVendedor,
                            vendedor = ficha.NombreVendedor,
                            autoCobrador = ficha.AutoCobrador,
                            codigoCobrador = ficha.CodigoCobrador,
                            cobrador = ficha.NombreCobrador,
                            autoTransporte = ficha.AutoTransporte,
                            codigoTransporte = ficha.CodigoTransporte,
                            transporte = ficha.NombreTransporte,
                            montoRecibido = ficha.MontoRecibido,
                            cambioDar = ficha.CambioDar,
                            esCredito=ficha.IsCredito,
                            tarifa=ficha.Tarifa,
                            saldoPendiente=ficha.SaldoPendiente,
                            autoConceptoMov=ficha.AutoConceptoMov,
                            codigoConceptoMov=ficha.CodigoConceptoMov,
                            nombreConceptoMov=ficha.NombreConceptoMov,
                        };
                        cnn.Venta.Add(entVenta);
                        cnn.SaveChanges();


                        foreach (var rg in ficha.Items)
                        {
                            var entItem = new LibEntitySqLitePosOffLine.VentaDetalle()
                            {
                                idVenta = entVenta.id,
                                autoDepartamento = rg.AutoDepartamento,
                                autoGrupo = rg.AutoGrupo,
                                autoProducto = rg.AutoProducto,
                                autoSubGrupo = rg.AutoSubGrupo,
                                autoTasa = rg.AutoTasa,
                                cantidad = rg.Cantidad,
                                cantidadUnd = rg.CantidadUnd,
                                categoria = rg.Categoria,
                                codigoProducto = rg.CodigoPrd,
                                costoCompraUnd = rg.CostoCompraUnd,
                                costoPromedioUnd = rg.CostoPromedioUnd,
                                costoVenta = rg.CostoVenta,
                                decimales = rg.Decimales,
                                diaEmpaqueGarantia = rg.DiasEmpaqueGarantia,
                                empaqueContenido = rg.EmpaqueContenido,
                                empaqueDescripcion = rg.EmpaqueDescripcion,
                                empaqueCodigo=rg.EmpaqueCodigo,
                                montoDesc1 = rg.MontoDscto_1,
                                montoDesc2 = rg.MontoDscto_2,
                                montoDesc3 = rg.MontoDscto_3,
                                montoIva = rg.MontoIva,
                                NombreProducto = rg.NombrePrd,
                                notas = rg.Notas,
                                porctDesc1 = rg.PorcDscto_1,
                                porctDesc2 = rg.PorcDscto_2,
                                porctDesc3 = rg.PorcDscto_3,
                                precioFinal = rg.PrecioFinal,
                                precioItem = rg.PrecioItem,
                                precioNeto = rg.PrecioNeto,
                                precioSugerido = rg.PrecioSugerido,
                                precioUnd = rg.PrecioUnd,
                                tarifa = rg.TarifaPrecio,
                                tasaIva = rg.TasaIva,
                                total = rg.Total,
                                totalNeto = rg.TotalNeto,
                                utilidadMonto = rg.MontoUtilidad,
                                utilidadPorct = rg.PorctUtilidad,
                                totalDescuento = rg.TotalDescuento,
                                tipoIva=rg.TipoIva,
                                esPesado=rg.EsPesado,
                                costoCompra=rg.CostoCompra,
                                costoPromedio=rg.CostoPromedio,
                            };
                            cnn.VentaDetalle.Add(entItem);
                            cnn.SaveChanges();
                        }

                        if (ficha.MetodosPago != null) 
                        {
                            foreach (var mt in ficha.MetodosPago)
                            {
                                var entMetodo = new LibEntitySqLitePosOffLine.VentaPago()
                                {
                                    idVenta = entVenta.id,
                                    tipoMedioCobro=mt.tipoMedioPago,
                                    autoMedioCobro = mt.autoMedioPago,
                                    codioMedioCobro = mt.codigoMedioPago,
                                    descripMedioCobro = mt.descripcionMedioPago,
                                    importe = mt.Importe,
                                    montoRecibido = mt.MontoRecibido,
                                    tasa = mt.Tasa,
                                    lote = mt.Lote,
                                    referencia = mt.Referencia,
                                };
                                cnn.VentaPago.Add(entMetodo);
                                cnn.SaveChanges();
                            }
                        }

                        foreach (var ie in ficha.ItemsLimpiar)
                        {
                            var entItemEliminar = cnn.Item.Find(ie.Id);
                            if (entItemEliminar == null)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "ITEM VENTA NO ENCONTRADO ";
                                return result;
                            };
                            cnn.Item.Remove(entItemEliminar);
                            cnn.SaveChanges();
                        }

                        ts.Commit();
                        result.Id = (int)entVenta.id;
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

        public DtoLib.ResultadoLista<DtoLibPosOffLine.VentaDocumento.Lista.Resumen> VentaDocumento_Lista(DtoLibPosOffLine.VentaDocumento.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPosOffLine.VentaDocumento.Lista.Resumen>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var q = cnn.Venta.ToList();
                    if (filtro.IdJornada != -1) 
                    {
                        q = q.Where(w => w.idJornada == filtro.IdJornada).ToList();
                    }

                    var list = new List<DtoLibPosOffLine.VentaDocumento.Lista.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            result.Lista = q.Select(s =>
                            {
                                var isActivo = s.estatusActivo == 1 ? true : false;
                                var tipoDocumento = DtoLibPosOffLine.VentaDocumento.Lista.Enumerados.EnumTipoDocumento.SinDefinir;
                                switch (s.tipoDocumento) 
                                {
                                    case 1:
                                        tipoDocumento = DtoLibPosOffLine.VentaDocumento.Lista.Enumerados.EnumTipoDocumento.Factura;
                                        break;
                                    case 2:
                                        tipoDocumento = DtoLibPosOffLine.VentaDocumento.Lista.Enumerados.EnumTipoDocumento.NotaDebito;
                                        break;
                                    case 3:
                                        tipoDocumento = DtoLibPosOffLine.VentaDocumento.Lista.Enumerados.EnumTipoDocumento.NotaCredito;
                                        break;
                                }
                                var r = new DtoLibPosOffLine.VentaDocumento.Lista.Resumen()
                                {
                                     Id=(int)s.id,
                                     Documento=s.documento,
                                     Control=s.control,
                                     FechaEmision=DateTime.Parse(s.fecha),
                                     HoraEmision=s.hora,
                                     NombreRazonSocial=s.nombreRazonSocial,
                                     CiRif=s.ciRif,
                                     Monto=s.montoTotal,
                                     TipoDocumento=tipoDocumento,
                                     IsActivo=isActivo,
                                     Signo=(int)s.signo,
                                     Renglones=(int)s.renglones,
                                     Serie=s.serie,
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

        public DtoLib.Resultado VentaDocumento_Anular(int idDocumento)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var entVenta = cnn.Venta.Find(idDocumento);
                    if (entVenta == null) 
                    {
                        result.Mensaje = "[ ID ] DOCUMENTO DE VENTA, NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    if (entVenta.estatusActivo==0)
                    {
                        result.Mensaje = "DOCUMENTO DE VENTA, YA SE ENCUENTRA ANULADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    entVenta.estatusActivo = 0;
                    cnn.SaveChanges();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.VentaDocumento.Cargar.Ficha> VentaDocumento_Cargar(int idDocumento)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.VentaDocumento.Cargar.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var q = cnn.Venta.Find(idDocumento);
                    var qd = cnn.VentaDetalle.Where(f=>f.idVenta==idDocumento).ToList();
                    var qp = cnn.VentaPago.Where(f => f.idVenta == idDocumento).ToList();

                    if (qd==null)
                    {
                        result.Entidad=null;
                        result.Mensaje="DETALLES/ITEM NO ENCONTRADOS";
                        result.Result= DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    if (qd.Count==0)
                    {
                        result.Entidad=null;
                        result.Mensaje="DETALLES/ITEM NO ENCONTRADOS";
                        result.Result= DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    if (q != null)
                    {
                        var s = q;
                        var isActivo = s.estatusActivo == 1 ? true : false;
                        var isCredito = s.esCredito.Trim().ToUpper() == "S" ? true : false;
                        var tipoDocumento = DtoLibPosOffLine.VentaDocumento.Cargar.Enumerados.EnumTipoDocumento.SinDefinir;
                        switch (s.tipoDocumento)
                        {
                            case 1:
                                tipoDocumento = DtoLibPosOffLine.VentaDocumento.Cargar.Enumerados.EnumTipoDocumento.Factura;
                                break;
                            case 2:
                                tipoDocumento = DtoLibPosOffLine.VentaDocumento.Cargar.Enumerados.EnumTipoDocumento.NotaDebito;
                                break;
                            case 3:
                                tipoDocumento = DtoLibPosOffLine.VentaDocumento.Cargar.Enumerados.EnumTipoDocumento.NotaCredito;
                                break;
                        }

                        var nr = new DtoLibPosOffLine.VentaDocumento.Cargar.Ficha()
                        {
                            AnoRelacion = s.anoRelacion,
                            Aplica = s.aplica,
                            Base1 = s.base1,
                            Base2 = s.base2,
                            Base3 = s.base3,
                            CambioDar = s.cambioDar,
                            CargoMonto_1 = s.cargoMonto1,
                            CargoPorc_1 = s.cargoPorc_1,
                            CiRif = s.ciRif,
                            ClienteDirFiscal = s.dirFiscal,
                            ClienteId =(int) s.idCliente,
                            ClienteNombre = s.nombreRazonSocial,
                            ClienteTelefono = s.telefono,
                            CobradorAuto = s.autoCobrador,
                            CobradorCodigo = s.codigoCobrador,
                            CobradorNombre = s.cobrador,
                            CodigoSucursal = s.codigoSucursal,
                            Control = s.control,
                            DepositoAuto = s.autoDeposito,
                            DepositoCodigo = s.codigoDeposito,
                            DepositoNombre = s.deposito,
                            DesctoMonto_1 = s.descuentoMonto1,
                            DesctoMonto_2 = s.descuentoMonto2,
                            DesctoPorc_1 = s.descuentoPorc1,
                            DesctoPorc_2 = s.descuentoPorc2,
                            Documento = s.documento,
                            Estacion = s.estacion,
                            FactorCambio = s.factorCambio,
                            Fecha = DateTime.Parse(s.fecha),
                            Hora = s.hora,
                            Impuesto1 = s.impuesto1,
                            Impuesto2 = s.impuesto2,
                            Impuesto3 = s.impuesto3,
                            IsActiva = isActivo,
                            IsCredito = isCredito,
                            MesRelacion = s.mesRelacion,
                            MontoBase = s.montoBase,
                            MontoCostoVenta = s.montoCostoVenta,
                            MontoDivisa = s.montoDivisa,
                            MontoExento = s.montoExento,
                            MontoImpuesto = s.montoImpuesto,
                            MontoRecibido = s.montoRecibido,
                            MontoSubt = s.montoSubTotal,
                            MontoSubtImpuesto = s.montoSubTotalImpuesto,
                            MontoSubtNeto = s.montoSubTotalNeto,
                            MontoTotal = s.montoTotal,
                            MontoUtilidad = s.montoUtilidad,
                            MontoUtilidadPorc = s.montoUtilidadPorc,
                            MontoVentaNeta = s.montoVentaNeta,
                            Renglones = (int)s.renglones,
                            Serie = s.serie,
                            Signo = (int)s.signo,
                            TasaIva1 = s.tasaIva1,
                            TasaIva2 = s.tasaIva2,
                            TasaIva3 = s.tasaIva3,
                            TipoDocumento = tipoDocumento,
                            TranporteAuto = s.autoTransporte,
                            TranporteCodigo = s.codigoTransporte,
                            TranporteNombre = s.transporte,
                            UsuarioAuto = s.autoUsuario,
                            UsuarioCodigo = s.usuarioCodigo,
                            UsuarioNombre = s.usuario,
                            VendedorAuto = s.autoVendedor,
                            VendedorCodigo = s.codigoVendedor,
                            VendedorNombre = s.vendedor,
                            Tarifa=s.tarifa,
                            SaldoPendiente=s.saldoPendiente,
                            AutoConceptoMov =s.autoConceptoMov,
                            CodigoConceptoMov =s.codigoConceptoMov,
                            NombreConceptoMov =s.nombreConceptoMov,
                        };

                        var det = qd.Select(t =>
                        {
                            var esPesado = t.esPesado == 1 ? true : false;
                            var rg = new DtoLibPosOffLine.VentaDocumento.Cargar.Detalle()
                            {
                                AutoDepartamento = t.autoDepartamento,
                                AutoGrupo = t.autoGrupo,
                                AutoProducto = t.autoProducto,
                                AutoSubGrupo = t.autoSubGrupo,
                                AutoTasa = t.autoTasa,
                                Cantidad = t.cantidad,
                                CantidadUnd = t.cantidadUnd,
                                Categoria = t.categoria,
                                CodigoProducto = t.codigoProducto,
                                CostoCompraUnd = t.costoCompraUnd,
                                CostoPromedioUnd = t.costoPromedioUnd,
                                CostoVenta = t.costoVenta,
                                Decimales = t.decimales,
                                DiaEmpaqueGarantia = (int)t.diaEmpaqueGarantia,
                                EmpaqueContenido = (int)t.empaqueContenido,
                                EmpaqueDescripcion = t.empaqueDescripcion,
                                EmpaqueCodigo=t.empaqueCodigo,
                                Id = (int)t.id,
                                MontoDscto_1 = t.montoDesc1,
                                MontoDscto_2 = t.montoDesc2,
                                MontoDscto_3 = t.montoDesc3,
                                MontoIva = t.montoIva,
                                NombreProducto = t.NombreProducto,
                                Notas = t.notas,
                                PorcDscto_1 = t.porctDesc1,
                                PorcDscto_2 = t.porctDesc2,
                                PorcDscto_3 = t.porctDesc3,
                                PrecioFinal = t.precioFinal,
                                PrecioItem = t.precioItem,
                                PrecioNeto = t.precioNeto,
                                PrecioSugerido = t.precioSugerido,
                                PrecioUnd = t.precioSugerido,
                                Tarifa = t.tarifa,
                                TasaIva = t.tasaIva,
                                Total = t.total,
                                TotalDescuento = t.totalDescuento,
                                TotalNeto = t.totalNeto,
                                UtilidadMonto = t.utilidadMonto,
                                UtilidadPorct = t.utilidadPorct,
                                EsPesado=esPesado,
                                TipoIva=t.tipoIva,
                                CostoCompra=t.costoCompra,
                                CostoPromedio=t.costoPromedio,
                            };
                            return rg;
                        }).ToList();
                        nr.Detalles = det;

                        nr.MediosPago = new List<DtoLibPosOffLine.VentaDocumento.Cargar.Pago>();
                        if (qp != null) 
                        {
                            if (qp.Count > 0) 
                            {
                                var pag = qp.Select(t =>
                                {
                                    var np = new DtoLibPosOffLine.VentaDocumento.Cargar.Pago()
                                    {
                                        Codigo = t.codioMedioCobro,
                                        Descripcion = t.descripMedioCobro,
                                        Importe = t.importe,
                                        Lote = t.lote,
                                        MontoRecibido = t.montoRecibido,
                                        Referencia = t.referencia,
                                        Tasa = t.tasa,
                                    };
                                    return np;
                                }).ToList();
                                nr.MediosPago = pag;
                            }
                        }

                        result.Entidad = nr;
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

    }

}
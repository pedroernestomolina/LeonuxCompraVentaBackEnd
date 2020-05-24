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
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select date('now')").FirstOrDefault();
                        var mesRelacion = fechaSistema.Month.ToString().Trim().PadLeft(2, '0');
                        var anoRelacion = fechaSistema.Year.ToString();

                        var entVenta = new LibEntitySqLitePosOffLine.Venta()
                        {
                            documento = ficha.Documento,
                            fecha = fechaSistema.ToShortDateString(),
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
                            hora = fechaSistema.ToShortTimeString(),
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

    }

}
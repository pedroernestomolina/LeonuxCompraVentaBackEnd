using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{

    public partial class Provider : IPosOffLine.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibPosOffLine.Reporte.Pago.Detalle.Ficha> Reporte_Pago_Detalle(DtoLibPosOffLine.Reporte.Pago.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPosOffLine.Reporte.Pago.Detalle.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var idOp = filtro.IdOperador;
                    var mov = cnn.Venta.
                        GroupJoin(cnn.VentaPago, v => v.id, vp => vp.idVenta, (v, vp) => new { v, vp }).
                        Where(w => w.v.idOperador == idOp).
                        ToList();

                    var list = new List<DtoLibPosOffLine.Reporte.Pago.Detalle.Ficha>();
                    if (mov != null)
                    {
                        if (mov.Count > 0)
                        {
                            list = mov.Select(s =>
                            {
                                var _estatus = DtoLibPosOffLine.Reporte.Pago.Enumerados.enumEstatus.Activo;
                                if (s.v.estatusActivo != 1)
                                {
                                    _estatus = DtoLibPosOffLine.Reporte.Pago.Enumerados.enumEstatus.Anulado;
                                }
                                var _tipoDoc = DtoLibPosOffLine.Reporte.Pago.Enumerados.enumTipoDocumento.Factura;
                                if (s.v.tipoDocumento != 1)
                                {
                                    _tipoDoc = DtoLibPosOffLine.Reporte.Pago.Enumerados.enumTipoDocumento.NotaCredito;
                                }

                                var lp = new List<DtoLibPosOffLine.Reporte.Pago.Detalle.Pago>();
                                lp = s.vp.Select(sp =>
                                {
                                    var pg = new DtoLibPosOffLine.Reporte.Pago.Detalle.Pago()
                                    {
                                        codigo = sp.codioMedioCobro,
                                        descripcion = sp.descripMedioCobro,
                                        importe = sp.importe,
                                        lote = sp.lote,
                                        montoRecibido = sp.montoRecibido,
                                        referencia = sp.referencia,
                                        tasa = sp.tasa,
                                        tipoMedioCobro = (DtoLibPosOffLine.Reporte.Pago.Enumerados.enumTipoMedioCobro)sp.tipoMedioCobro,
                                    };

                                    return pg;
                                }).ToList();

                                var nr = new DtoLibPosOffLine.Reporte.Pago.Detalle.Ficha()
                                {
                                    id=(int)s.v.id,
                                    aplica = s.v.aplica,
                                    ciRif = s.v.ciRif,
                                    dirFiscal = s.v.dirFiscal,
                                    documento = s.v.documento,
                                    estatus = _estatus,
                                    fecha = DateTime.Parse(s.v.fecha),
                                    hora= s.v.hora,
                                    signo= (int)s.v.signo,
                                    monto = s.v.montoTotal,
                                    nombreRazaonSocial = s.v.nombreRazonSocial,
                                    telefono = s.v.telefono,
                                    tipoDoc = _tipoDoc,
                                    cambioDar= s.v.cambioDar,
                                };
                                nr.pagos = lp;

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

        public DtoLib.ResultadoLista<DtoLibPosOffLine.Reporte.NCredito.Ficha> Reporte_NCredito(DtoLibPosOffLine.Reporte.NCredito.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPosOffLine.Reporte.NCredito.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var idOp = filtro.IdOperador;
                    var mov = cnn.Venta.
                        Where(w => w.idOperador == idOp && w.tipoDocumento==3).
                        ToList();

                    var list = new List<DtoLibPosOffLine.Reporte.NCredito.Ficha>();
                    if (mov != null)
                    {
                        if (mov.Count > 0)
                        {
                            list = mov.Select(s =>
                            {
                                var _estatus = DtoLibPosOffLine.Reporte.NCredito.Enumerados.enumEstatus.Activo;
                                if (s.estatusActivo != 1)
                                {
                                    _estatus = DtoLibPosOffLine.Reporte.NCredito.Enumerados.enumEstatus.Anulado;
                                }
                                var _tipoDoc = DtoLibPosOffLine.Reporte.NCredito.Enumerados.enumTipoDocumento.Factura;
                                if (s.tipoDocumento != 1)
                                {
                                    _tipoDoc = DtoLibPosOffLine.Reporte.NCredito.Enumerados.enumTipoDocumento.NotaCredito;
                                }

                                var nr = new DtoLibPosOffLine.Reporte.NCredito.Ficha()
                                {
                                    id = (int)s.id,
                                    aplica = s.aplica,
                                    ciRif = s.ciRif,
                                    dirFiscal = s.dirFiscal,
                                    documento = s.documento,
                                    estatus = _estatus,
                                    fecha = DateTime.Parse(s.fecha),
                                    hora = s.hora,
                                    signo = (int)s.signo,
                                    monto = s.montoTotal,
                                    nombreRazaonSocial = s.nombreRazonSocial,
                                    telefono = s.telefono,
                                    tipoDoc = _tipoDoc,
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

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Reporte.Pago.Resumen.Ficha> Reporte_Pago_Resumen(DtoLibPosOffLine.Reporte.Pago.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Reporte.Pago.Resumen.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var idOp = filtro.IdOperador;

                    var nCredito = cnn.Venta.
                        Where(w => w.idOperador == idOp && w.estatusActivo == 1 && w.tipoDocumento == 3).
                        Sum(s => s.montoTotal);

                    var nCambioDar = cnn.Venta.
                        Where(w => w.idOperador == idOp && w.estatusActivo == 1).Sum(s => s.cambioDar);

                    var mov = cnn.Venta.
                        Join(cnn.VentaPago, v => v.id, vp => vp.idVenta, (v, vp) => new { v, vp }).
                        Where(w => w.v.idOperador == idOp && w.v.estatusActivo==1 && w.v.tipoDocumento==1).
                        ToList();

                    var list = new List<DtoLibPosOffLine.Reporte.Pago.Resumen.Detalle>();
                    if (mov != null)
                    {
                        if (mov.Count > 0)
                        {
                            list = mov.Select(s =>
                            {
                                var nr = new DtoLibPosOffLine.Reporte.Pago.Resumen.Detalle()
                                {
                                    codigo = s.vp.codioMedioCobro,
                                    descripcion = s.vp.descripMedioCobro,
                                    importe = s.vp.importe,
                                    lote = s.vp.lote,
                                    montoRecibido = s.vp.montoRecibido,
                                    referencia = s.vp.referencia,
                                    tasa = s.vp.tasa,
                                    tipoMedioCobro = (DtoLibPosOffLine.Reporte.Pago.Enumerados.enumTipoMedioCobro)s.vp.tipoMedioCobro,
                                };

                                return nr;
                            }).ToList();
                        }
                    }

                    var reg = new DtoLibPosOffLine.Reporte.Pago.Resumen.Ficha()
                    {
                        montoNCredito = nCredito,
                        montoCambioDar = nCambioDar,
                        detalle = list,
                    };
                    result.Entidad = reg;
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
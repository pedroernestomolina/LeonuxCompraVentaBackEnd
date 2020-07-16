using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{
    
    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibInventario.Kardex.Movimiento.Resumen> Producto_Kardex_Movimiento_Lista(DtoLibInventario.Kardex.Movimiento.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Kardex.Movimiento.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var fechaServidor = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                    var q = cnn.productos_kardex.Where(f => f.auto_producto == filtro.autoProducto).ToList();

                    if (filtro.ultDias != DtoLibInventario.Kardex.Enumerados.EnumMovUltDias.SinDefinir)
                    {
                        DateTime? desde=fechaServidor.Date;
                        var hasta=fechaServidor.Date;
                        switch (filtro.ultDias) 
                        {
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias.Hoy:
                                desde= desde.Value.AddDays(0);
                                q = q.Where(f => f.fecha >= desde ).ToList();
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias.Ayer:
                                desde = desde.Value.AddDays(-1);
                                q = q.Where(f => f.fecha >= desde ).ToList();
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._7Dias:
                                desde = desde.Value.AddDays(-7);
                                q = q.Where(f => f.fecha >= desde ).ToList();
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._15Dias:
                                desde = desde.Value.AddDays(-15);
                                q = q.Where(f => f.fecha >= desde ).ToList();
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._30Dias:
                                desde = desde.Value.AddDays(-30);
                                q = q.Where(f => f.fecha >= desde ).ToList();
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._45Dias:
                                desde = desde.Value.AddDays(-45);
                                q = q.Where(f => f.fecha >= desde ).ToList();
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._60Dias:
                                desde = desde.Value.AddDays(-60);
                                q = q.Where(f => f.fecha >= desde ).ToList();
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._90Dias:
                                desde = desde.Value.AddDays(-90);
                                q = q.Where(f => f.fecha >= desde).ToList();
                                break;
                        }
                    }

                    var list = new List<DtoLibInventario.Kardex.Movimiento.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var _isAnulado = s.estatus_anulado.Trim().ToUpper() == "1" ? true : false;
                                var _modulo = DtoLibInventario.Kardex.Enumerados.EnumModulo.SinDefinir;
                                switch (s.modulo.Trim().ToUpper()) 
                                {
                                    case "INVENTARIO":
                                        _modulo = DtoLibInventario.Kardex.Enumerados.EnumModulo.Inventario;
                                        break;
                                    case "COMPRAS":
                                        _modulo = DtoLibInventario.Kardex.Enumerados.EnumModulo.Compra;
                                        break;
                                    case "VENTAS":
                                        _modulo = DtoLibInventario.Kardex.Enumerados.EnumModulo.Venta;
                                        break;
                                }
                                var _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas .SinDefinir;
                                switch (s.siglas.Trim().ToUpper())
                                {
                                    case "FAC":
                                        _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas.Factura;
                                        break;
                                    case "NCR":
                                        _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas.NCredito;
                                        break;
                                    case "NDB":
                                        _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas.NDebito;
                                        break;
                                    case "NEN":
                                        _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas.NEntrega;
                                        break;
                                    case "CAR":
                                        _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas.Cargo;
                                        break;
                                    case "DES":
                                        _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas.Descargo;
                                        break;
                                    case "TRA":
                                        _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas.Traslado;
                                        break;
                                    case "AJU":
                                        _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas.Ajuste;
                                        break;
                                }


                                var r = new DtoLibInventario.Kardex.Movimiento.Resumen()
                                {
                                    autoConcepto = s.auto_concepto,
                                    autoDeposito = s.auto_deposito,
                                    autoDocumento = s.auto_documento,
                                    cantidad = s.cantidad,
                                    cantidadBono = s.cantidad_bono,
                                    cantidadUnd = s.cantidad_und,
                                    codigoDoc = s.codigo,
                                    codigoSucursal = s.codigo_sucursal,
                                    costoUnd = s.costo_und,
                                    documento = s.documento,
                                    entidad = s.entidad,
                                    fecha = s.fecha,
                                    hora = s.hora,
                                    isAnulado = _isAnulado,
                                    Modulo = _modulo,
                                    moduloDoc = s.modulo,
                                    nota = s.nota,
                                    precioUnd = s.precio_und,
                                    Siglas = _siglas,
                                    siglasDoc = s.siglas,
                                    signoDoc = s.signo,
                                    total = s.total,
                                };
                                return r;
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

    }

}
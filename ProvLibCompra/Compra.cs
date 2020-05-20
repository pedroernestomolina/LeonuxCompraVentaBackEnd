using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibCompra
{

    public partial class Provider: ILibCompras.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibCompra.Compra.Resumen> CompraLista(DtoLibCompra.Compra.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibCompra.Compra.Resumen>();

            try
            {
                using (var cnn=new LibEntityCompras.libComprasEntities(_cnCompra.ConnectionString))
                {
                    var q = cnn.compras.ToList();
                    if (filtro.segun_FechaEmisionDesde.HasValue)
                    {
                        q = q.Where(w => w.fecha >= filtro.segun_FechaEmisionDesde.Value).ToList();
                    }
                    if (filtro.segun_FechaEmisionHasta.HasValue)
                    {
                        q = q.Where(w => w.fecha >= filtro.segun_FechaEmisionHasta.Value).ToList();
                    }

                    var list = new List<DtoLibCompra.Compra.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            result.Lista = q.Select(s =>
                            {
                                var isAnulado=s.estatus_anulado=="1"?true:false;
                                var isMercancia=s.auto_concepto=="0000000001"?true:false;
                                var isCerradoContable=s.estatus_cierre_contable=="1"?true:false;
                                var tipoDoc=DtoLibCompra.Enumerados.enumTipoDocumento.SinDefinir;
                                var situacionDoc=DtoLibCompra.Enumerados.enumSituacionDocumento.SinDefinir;
                                var isCredito = s.condicion_pago.Trim().ToUpper() == "CREDITO" ? true : false;
                                switch (s.tipo.Trim().ToUpper())
                                {
                                    case "01":
                                        tipoDoc= DtoLibCompra.Enumerados.enumTipoDocumento.Factura;
                                        situacionDoc= DtoLibCompra.Enumerados.enumSituacionDocumento.Procesado;
                                        break;
                                    case "02":
                                        tipoDoc= DtoLibCompra.Enumerados.enumTipoDocumento.NotaDebito;
                                        situacionDoc= DtoLibCompra.Enumerados.enumSituacionDocumento.Procesado;
                                        break;
                                    case "03":
                                        tipoDoc= DtoLibCompra.Enumerados.enumTipoDocumento.NotaCredito;
                                        situacionDoc= DtoLibCompra.Enumerados.enumSituacionDocumento.Procesado;
                                        break;
                                    case "04":
                                        tipoDoc= DtoLibCompra.Enumerados.enumTipoDocumento.OrdenCompra;
                                        situacionDoc= DtoLibCompra.Enumerados.enumSituacionDocumento.Transito;
                                        break;
                                    case "05":
                                        tipoDoc= DtoLibCompra.Enumerados.enumTipoDocumento.Recepcion;
                                        situacionDoc= DtoLibCompra.Enumerados.enumSituacionDocumento.Procesado;
                                        break;
                                }

                                var r = new DtoLibCompra.Compra.Resumen()
                                {
                                     Auto=s.auto,
                                     CodigoSucursal=s.codigo_sucursal,
                                     ControlNro=s.control,
                                     DocumentoNro=s.documento,
                                     EntidadNombre=s.razon_social,
                                     EntidadRif=s.ci_rif,
                                     FechaEmision=s.fecha,
                                     FechaRecepcion=null,
                                     FechaRegistro=s.fecha_registro,
                                     IsAnulado=isAnulado,
                                     IsCredito=isCredito,
                                     IsMercancia=isMercancia,
                                     IsCerradoContable=isCerradoContable,
                                     Situacion=situacionDoc,
                                     TipoDocumento=tipoDoc,
                                     Total=s.total,
                                     Notas=s.nota,
                                     Signo=s.signo,
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
                        result.Lista= list;
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
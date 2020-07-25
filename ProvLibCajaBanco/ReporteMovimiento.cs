using LibEntityCajaBanco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibCajaBanco
{

    public partial class Provider : ILibCajaBanco.IProvider 
    {

        public DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.ArqueoCajaPos.Ficha> CajaBanco_ArqueoCajaPos(DtoLibCajaBanco.Reporte.Movimiento.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.ArqueoCajaPos.Ficha>();

            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {
                    var list = new List<DtoLibCajaBanco.Reporte.Movimiento.ArqueoCajaPos.Ficha>();
                    var mov = cnn.pos_arqueo.ToList();
                    if (filtro.desdeFecha.HasValue)
                    {
                        var desde = filtro.desdeFecha.Value;
                        mov = mov.Where(f => f.fecha >= desde).ToList();
                    }
                    if (filtro.hastaFecha.HasValue)
                    {
                        var hasta = filtro.hastaFecha.Value;
                        mov = mov.Where(f => f.fecha <= hasta).ToList();
                    }
                    if (filtro.autoSucursal.Trim() != "")
                    {
                        mov = mov.Where(f => f.auto_cierre.Substring(0, 2) == filtro.autoSucursal).ToList();
                    }

                    if (mov != null)
                    {
                        if (mov.Count() > 0)
                        {
                            list = mov.Select(s =>
                            {
                                var lDivisa = cnn.cxc_medio_pago.Where(w => w.cierre == s.auto_cierre &&
                                    w.estatus_anulado == "0" && w.codigo == "02").Select(st => st.lote).ToList();
                                var tcntDivisa = 0;
                                foreach (var dv in lDivisa)
                                {
                                    var cnt = int.Parse(dv);
                                    tcntDivisa += cnt;
                                };

                                var rt = new DtoLibCajaBanco.Reporte.Movimiento.ArqueoCajaPos.Ficha()
                                {
                                    autoCierre= s.auto_cierre.Substring(4),
                                    sucursal = s.auto_cierre.Substring(0, 2),
                                    equipo = s.auto_cierre.Substring(2, 2),
                                    autoUsuario = s.auto_usuario,
                                    codigoUsuario = s.codigo,
                                    fecha = s.fecha,
                                    hora = s.hora,
                                    nombreUsuario = s.usuario,
                                    diferencia = s.diferencia,
                                    efectivo = s.efectivo,
                                    divisa = s.cheque,
                                    tarjeta = s.debito,
                                    otros = s.otros,
                                    firma = s.firma,
                                    devolucion = s.devolucion,
                                    subtotal = s.subtotal,
                                    total = s.total,
                                    mefectivo = s.mefectivo,
                                    mdivisa = s.mcheque,
                                    mtarjeta = s.mtarjeta,
                                    motros = s.motros,
                                    msubtotal = s.msubtotal,
                                    mtotal = s.mtotal,
                                    cntdivisa = tcntDivisa,
                                };
                                return rt;
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
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

        //public class suc
        //{
        //    public string sucursal { get; set; }
        //    public List<equipo> equipos { get; set; }
        //}

        //public class equipo 
        //{
        //    public string id { get; set;  }
        //    public List<arqueo> arqueos { get; set; }
        //}

        //public class arqueo 
        //{
        //    public string sucursal { get; set; }
        //    public string equipo { get; set; }
        //    public string autoUsuario { get; set; }
        //    public string codigoUsuario { get; set; }
        //    public string nombreUsuario { get; set; }
        //    public DateTime fecha { get; set; }
        //    public string hora { get; set; }
        //}


        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.Movimientos.ArqueoCajaPos.Ficha> CajaBanco_ArqueoVentaPos(DtoLibInventario.Reportes.Movimientos.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.Movimientos.ArqueoCajaPos.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var list = new List<DtoLibInventario.Reportes.Movimientos.ArqueoCajaPos.Ficha>();
                    var mov = cnn.pos_arqueo.ToList();
                    if (filtro.desdeFecha.HasValue) 
                    {
                        var desde=filtro.desdeFecha.Value;
                        mov = mov.Where(f => f.fecha >= desde).ToList();
                    }
                    if (filtro.hastaFecha.HasValue)
                    {
                        var hasta = filtro.hastaFecha.Value;
                        mov = mov.Where(f => f.fecha <= hasta).ToList();
                    }
                    if (filtro.autoSucursal.Trim()!="")
                    {
                        mov = mov.Where(f => f.auto_cierre.Substring(0,2)==filtro.autoSucursal).ToList();
                    }

                    if (mov != null)
                    {
                        if (mov.Count() > 0)
                        {
                            list = mov.Select(s =>
                            {
                                var lDivisa = cnn.cxc_medio_pago.Where(w => w.cierre == s.auto_cierre &&
                                    w.estatus_anulado == "0" && w.codigo == "02").Select(st=>st.lote).ToList();
                                var tcntDivisa = 0;
                                foreach (var dv in lDivisa) 
                                {
                                    var cnt = int.Parse(dv);
                                    tcntDivisa += cnt;
                                };

                                var rt = new DtoLibInventario.Reportes.Movimientos.ArqueoCajaPos.Ficha()
                                {
                                    autoCierre = s.auto_cierre.Substring(4),
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
                                    cntdivisa=tcntDivisa,
                                };
                                return rt;
                            }).ToList();
                        }
                    }
                    result.Lista = list;

                    //var rt = new List<suc>();
                    //var listSuc = list.GroupBy(g => g.sucursal).Select(s=>s.Key).ToList();
                    //foreach (var it in listSuc) 
                    //{
                    //    var arq = list.Where(s=>s.sucursal==it).ToList();
                    //    var arqEq = arq.GroupBy(g=>g.equipo).Select(s=>s.Key).ToList();

                    //    var pt = new suc();
                    //    pt.sucursal = it;
                    //    pt.equipos = new List<equipo>();

                    //    foreach (var xt in arqEq) 
                    //    {
                    //        var equipo = new equipo();
                    //        equipo.id = xt;
                    //        equipo.arqueos = new List<arqueo>();

                    //        var arqSucEq = arq.Where(w => w.equipo == xt).ToList();
                    //        foreach (var zt in arqSucEq) 
                    //        {
                    //            equipo.arqueos.Add(zt);
                    //        }

                    //        pt.equipos.Add(equipo);
                    //    }
                    //    rt.Add(pt);
                    //}
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
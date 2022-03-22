using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoId Transito_Movimiento_Agregar(DtoLibInventario.Transito.Movimiento.Agregar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var s=ficha.mov;
                        var entMov = new productos_movimientos_transito()
                        {
                            autoriza = s.autoriza,
                            cntRenglones = s.cntRenglones,
                            codigoMov = s.codigoMov,
                            desConcepto = s.descConcepto,
                            desDepDestino = s.descDepDestino,
                            desDepOrigen = s.descDepOrigen,
                            desMov = s.descMov,
                            desSucDestino = s.descSucDestino,
                            desSucOrigen = s.descSucOrigen,
                            desUsuario = s.descUsuario,
                            estacionEquipo = s.estacionEquipo,
                            factorCambio = s.factorCambio,
                            fecha = fechaSistema.Date,
                            idConcepto = s.idConcepto,
                            idDepDestino = s.idDepDestino,
                            idDepOrigen = s.idDeOrigen,
                            idSucDestino = s.idSucDestino,
                            idSucOrigen = s.idSucOrigen,
                            monto = s.monto,
                            montoDivisa = s.montoDivisa,
                            motivo = s.motivo,
                            tipoMov = s.tipoMov,
                        };
                        cnn.productos_movimientos_transito.Add(entMov);
                        cnn.SaveChanges();


                        ts.Complete();
                        result.Id = entMov.id;
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
        public DtoLib.ResultadoEntidad<DtoLibInventario.Transito.Movimiento.Entidad.Ficha> 
            Transito_Movimiento_GetById(int idMov)
        {
                return null;
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Transito.Movimiento.Lista.Ficha> 
            Transito_Movimiento_GetLista(DtoLibInventario.Transito.Movimiento.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Transito.Movimiento.Lista.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q = cnn.productos_movimientos_transito.
                        Where(w => w.codigoMov == filtro.codMov && w.tipoMov == filtro.tipMov).
                        ToList();
                    var lst = new List<DtoLibInventario.Transito.Movimiento.Lista.Ficha>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            lst = q.Select(s =>
                            {
                                var r = new DtoLibInventario.Transito.Movimiento.Lista.Ficha()
                                {
                                };
                                return r;
                            }).ToList();
                        }
                    }
                    result.Lista = lst;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.Resultado Transito_Movimiento_AnularById(int idMov)
        {
                return null;
        }
        public DtoLib.ResultadoEntidad<int> Transito_Movimiento_GetCnt(DtoLibInventario.Transito.Movimiento.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<int>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q = cnn.productos_movimientos_transito.
                        Where(w=>w.codigoMov==filtro.codMov && w.tipoMov==filtro.tipMov).
                        ToList();
                    result.Entidad = q.Count();
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
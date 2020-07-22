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

        public DtoLib.ResultadoLista<DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Ficha> 
            Tools_AjusteNivelMinimoMaximo_GetLista(DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q = cnn.productos_deposito.
                        Join(cnn.productos,
                        pd => pd.auto_producto, p => p.auto,
                        (pd, p) => new { pd, p }).
                        Where(w => w.p.estatus.Trim().ToUpper() == "ACTIVO").ToList();

                    if (filtro.autoDeposito!="")
                    {
                        q = q.Where(w => w.pd.auto_deposito == filtro.autoDeposito).ToList();
                    }

                    var list = new List<DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Ficha>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var _decimales="0";
                                var _esPesado= s.p.estatus_pesado.Trim().ToUpper()=="1"?true:false;
                                var entEmp= cnn.productos_medida.Find(s.p.auto_empaque_compra);
                                if (entEmp!=null)
                                {
                                    _decimales=entEmp.decimales;
                                }

                                var r = new DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Ficha()
                                {
                                    autoProducto = s.p.auto,
                                    codigoProducto = s.p.codigo,
                                    decimales = _decimales,
                                    fisica = s.pd.fisica,
                                    nivelMinimo = s.pd.nivel_minimo,
                                    nivelOptimo = s.pd.nivel_optimo,
                                    nombreProducto = s.p.nombre,
                                    referenciaProducto = s.p.referencia,
                                    esPesado = _esPesado,
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

        public DtoLib.Resultado Tools_AjusteNivelMinimoMaximo_Ajustar(List<DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Ajustar.Ficha> listaAjuste)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "";
                        foreach (var it in  listaAjuste)
                        {
                            var entPrdDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == it.autoProducto && 
                                f.auto_deposito == it.autoDeposito );
                            if (entPrdDep == null)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje ="[ ID ] PRODUCTO / DEPOSITO NO ENCONTRADO";
                                return result;
                            }
                            entPrdDep.nivel_minimo = it.nivelMinimo;
                            entPrdDep.nivel_optimo = it.nivelOptimo;
                            cnn.SaveChanges();
                        };

                        ts.Complete();
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

    }

}
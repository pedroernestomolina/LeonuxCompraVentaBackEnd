using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{

    public partial class Provider : IPosOffLine.IProvider
    {

        public DtoLib.Resultado Pendiente_DejarCtaEnPendiente(DtoLibPosOffLine.Pendiente.DejarEnPendiente.Agregar ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    using (var ts = cnn.Database.BeginTransaction())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select date('now')").FirstOrDefault();

                        var entPend = new LibEntitySqLitePosOffLine.Pendiente()
                        {
                            idCliente = ficha.IdCliente,
                            fecha = fechaSistema.ToShortDateString(),
                            hora = fechaSistema.ToShortTimeString(),
                            renglones = ficha.Renglones,
                            monto = ficha.Monto,
                        };
                        cnn.Pendiente.Add(entPend);
                        cnn.SaveChanges();

                        foreach (var rg in ficha.Items)
                        {
                            var entItem = cnn.Item.Find(rg.IdItem);
                            if (entItem==null)
                            {
                                result.Mensaje = "ITEM [ ID ] NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            entItem.idPendiente = entPend.id;
                            cnn.SaveChanges();
                        }

                        ts.Commit();
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

        public DtoLib.ResultadoLista<DtoLibPosOffLine.Pendiente.Listar.Resumen> Pendiente_Lista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibPosOffLine.Pendiente.Listar.Resumen>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var q = cnn.Pendiente.ToList();
                    var list = new List<DtoLibPosOffLine.Pendiente.Listar.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            result.Lista = q.Select(s => 
                            {
                                var cliente="";
                                var entCli= cnn.Cliente.Find(s.idCliente);
                                if (entCli!=null)
                                {
                                    cliente=entCli.nombreRazonSocial;
                                }
                                var r = new DtoLibPosOffLine.Pendiente.Listar.Resumen()
                                {
                                    Id = (int)s.id,
                                    IdCliente=(int)s.idCliente,
                                    Cliente = cliente,
                                    Fecha = DateTime.Parse(s.fecha),
                                    Monto = s.monto,
                                    Renglones =(int) s.renglones,
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

        public DtoLib.Resultado Pendiente_AbrirCtaEnPendiente(int id)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    using (var ts = cnn.Database.BeginTransaction())
                    {
                        var entPend = cnn.Pendiente.Find(id);
                        if (entPend == null) 
                        {
                            result.Mensaje = "CUENTA PENDIENTE NO ENCONTRADA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var entItems = cnn.Item.Where(w => w.idPendiente == id).ToList();
                        if (entItems == null) 
                        {
                            result.Mensaje = "ITEMS NO DEFINIDOS";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (entItems.Count==0)
                        {
                            result.Mensaje = "ITEMS NO ENCONTRADOS";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        cnn.Pendiente.Remove(entPend);
                        foreach (var rg in entItems)
                        {
                            rg.idPendiente = -1;
                            cnn.SaveChanges();
                        }

                        ts.Commit();
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

        public DtoLib.Resultado Pendiente_EliminarCtaEnPendiente(int id)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    using (var ts = cnn.Database.BeginTransaction())
                    {
                        var entPend = cnn.Pendiente.Find(id);
                        if (entPend == null)
                        {
                            result.Mensaje = "CUENTA PENDIENTE NO ENCONTRADA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var entItems = cnn.Item.Where(w => w.idPendiente == id).ToList();
                        if (entItems == null)
                        {
                            result.Mensaje = "ITEMS NO DEFINIDOS";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (entItems.Count == 0)
                        {
                            result.Mensaje = "ITEMS NO ENCONTRADOS";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        cnn.Pendiente.Remove(entPend);
                        cnn.SaveChanges();

                        cnn.Item.RemoveRange(entItems);
                        cnn.SaveChanges();

                        ts.Commit();
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
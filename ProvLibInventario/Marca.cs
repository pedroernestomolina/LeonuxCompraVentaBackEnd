﻿using LibEntityInventario;
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

        public DtoLib.ResultadoLista<DtoLibInventario.Marca.Resumen> Marca_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Marca.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q = cnn.productos_marca.ToList();

                    var list = new List<DtoLibInventario.Marca.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var r = new DtoLibInventario.Marca.Resumen()
                                {
                                    auto = s.auto,
                                    nombre = s.nombre,
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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Marca.Ficha> Marca_GetFicha(string auto)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Marca.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.productos_marca.Find(auto);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] MARCA NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };
                    var nr = new DtoLibInventario.Marca.Ficha()
                    {
                        auto = ent.auto,
                        nombre = ent.nombre,
                    };
                    result.Entidad = nr;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoAuto Marca_Agregar(DtoLibInventario.Marca.Agregar ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "update sistema_contadores set a_productos_marca=a_productos_marca+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aMarca = cnn.Database.SqlQuery<int>("select a_productos_marca from sistema_contadores").FirstOrDefault();
                        var autoMarca = aMarca.ToString().Trim().PadLeft(10, '0');

                        var ent = new productos_marca()
                        {
                            auto = autoMarca,
                            nombre = ficha.nombre,
                        };
                        cnn.productos_marca.Add(ent);
                        cnn.SaveChanges();

                        ts.Complete();
                        result.Auto = autoMarca;
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

        public DtoLib.Resultado Marca_Editar(DtoLibInventario.Marca.Editar ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.productos_marca.Find(ficha.auto);
                        if (ent == null)
                        {
                            result.Mensaje = "[ ID ] ENTIDAD MARCA NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        ent.nombre = ficha.nombre;
                        cnn.SaveChanges();

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
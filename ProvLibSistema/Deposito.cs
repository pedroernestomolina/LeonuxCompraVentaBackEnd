using LibEntitySistema;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibSistema
{

    public partial class Provider : ILibSistema.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibSistema.Deposito.Resumen> Deposito_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibSistema.Deposito.Resumen>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var q = cnn.empresa_depositos.ToList();

                    var list = new List<DtoLibSistema.Deposito.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var _autoSuc="";
                                var _codSuc="";
                                var _suc="";
                                var entSuc = cnn.empresa_sucursal.FirstOrDefault(f=>f.codigo==s.codigo_sucursal);
                                if (entSuc != null) 
                                {
                                    _autoSuc=entSuc.auto;
                                    _codSuc=entSuc.codigo;
                                    _suc = entSuc.nombre;
                                }
                                var r = new DtoLibSistema.Deposito.Resumen()
                                {
                                    auto = s.auto,
                                    codigo = s.codigo,
                                    nombre = s.nombre,
                                    codigoSucursal = _codSuc,
                                    sucursal = _suc,
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

        public DtoLib.ResultadoEntidad<DtoLibSistema.Deposito.Ficha> Deposito_GetFicha(string auto)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Deposito.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.empresa_depositos.Find(auto);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] ENTIDAD DEPOSITO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var _autoSuc = "";
                    var _codSuc = "";
                    var _suc = "";
                    var entSuc = cnn.empresa_sucursal.FirstOrDefault(f => f.codigo == ent.codigo_sucursal);
                    if (entSuc != null)
                    {
                        _autoSuc = entSuc.auto;
                        _codSuc = entSuc.codigo;
                        _suc = entSuc.nombre;
                    }
                    var nr = new DtoLibSistema.Deposito.Ficha()
                    {
                        auto = ent.auto,
                        codigo = ent.codigo,
                        nombre = ent.nombre,
                        autoSucursal = _autoSuc,
                        codigoSucursal = _codSuc,
                        sucursal = _suc,
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

        public DtoLib.ResultadoAuto Deposito_Agregar(DtoLibSistema.Deposito.Agregar ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "update sistema_contadores set a_empresa_depositos=a_empresa_depositos+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aEmpresaDeposito = cnn.Database.SqlQuery<int>("select a_empresa_depositos from sistema_contadores").FirstOrDefault();
                        var autoEmpresaDeposito = aEmpresaDeposito.ToString().Trim().PadLeft(10, '0');

                        var ent = new empresa_depositos()
                        {
                            auto = autoEmpresaDeposito,
                            codigo = ficha.codigo,
                            nombre = ficha.nombre,
                            codigo_sucursal = ficha.codigoSucursal,
                        };
                        cnn.empresa_depositos.Add(ent);
                        cnn.SaveChanges();

                        var entExt = new empresa_depositos_ext()
                        {
                            auto_deposito = autoEmpresaDeposito,
                            es_activo = "1",
                            es_predeterminado = "",
                        };
                        cnn.empresa_depositos_ext.Add(entExt);
                        cnn.SaveChanges();

                        ts.Complete();
                        result.Auto = autoEmpresaDeposito;
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

        public DtoLib.Resultado Deposito_Editar(DtoLibSistema.Deposito.Editar ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.empresa_depositos.Find(ficha.auto);
                        if (ent == null)
                        {
                            result.Mensaje = "[ ID ] ENTIDAD DEPOSITO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        ent.codigo = ficha.codigo;
                        ent.nombre = ficha.nombre;
                        ent.codigo_sucursal = ficha.codigoSucursal;
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

        public DtoLib.ResultadoEntidad<int> Deposito_GeneraCodigoAutomatico()
        {
            var result = new DtoLib.ResultadoEntidad<int>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    int? aEmpresaDeposito = cnn.Database.SqlQuery<int?>("select a_empresa_depositos from sistema_contadores").FirstOrDefault();
                    if (!aEmpresaDeposito.HasValue)
                    {
                        result.Mensaje = "[ AUTOMATICO EMPRESA DEPOSITO ] PROBLEMA AL CONSULTAR CAMPO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = ((int)aEmpresaDeposito.Value)+1;
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
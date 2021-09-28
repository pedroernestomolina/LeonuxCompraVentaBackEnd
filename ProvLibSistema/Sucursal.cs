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

        public DtoLib.ResultadoLista<DtoLibSistema.Sucursal.Resumen> Sucursal_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibSistema.Sucursal.Resumen>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var q = cnn.empresa_sucursal.ToList();

                    var list = new List<DtoLibSistema.Sucursal.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var _grupo="";
                                var entGrupo = cnn.empresa_grupo.Find(s.autoEmpresaGrupo);
                                if (entGrupo != null) 
                                {
                                    _grupo = entGrupo.nombre;
                                }
                                var _deposito = "";
                                var entDeposito = cnn.empresa_depositos.Find(s.autoDepositoPrincipal);
                                if (entDeposito != null) 
                                {
                                    _deposito = entDeposito.nombre;
                                }

                                var r = new DtoLibSistema.Sucursal.Resumen()
                                {
                                    auto = s.auto,
                                    codigo = s.codigo,
                                    nombre = s.nombre,
                                    grupo = _grupo,
                                    deposito = _deposito,
                                    estatusFactMayor = s.estatus_facturar_mayor,
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

        public DtoLib.ResultadoEntidad<DtoLibSistema.Sucursal.Ficha> Sucursal_GetFicha(string auto)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Sucursal.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.empresa_sucursal.Find(auto);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] ENTIDAD SUCURSAL NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var autoGrupo="";
                    var nomGrupo="";
                    var idPrecioGrupo="";
                    var entGrupo= cnn.empresa_grupo.Find(ent.autoEmpresaGrupo);
                    if (entGrupo!=null)
                    {
                        autoGrupo=entGrupo.auto;
                        nomGrupo=entGrupo.nombre;
                        idPrecioGrupo=entGrupo.idPrecio;
                    };

                    var autoDep="";
                    var nomDep="";
                    var codDep="";
                    var entDep = cnn.empresa_depositos.Find(ent.autoDepositoPrincipal);
                    if (entDep!=null)
                    {
                        autoDep=entDep.auto;
                        nomDep=entDep.nombre;
                        codDep=entDep.codigo;
                    }

                    var nr = new DtoLibSistema.Sucursal.Ficha()
                    {
                        auto = ent.auto,
                        autoDepositoPrincipal = autoDep,
                        autoGrupoSucursal = autoGrupo,
                        codigo = ent.codigo,
                        codigoDepositoPrincipal = codDep,
                        nombreDepositoPrincipal = nomDep,
                        nombreGrupoSucursal = nomGrupo,
                        nombre = ent.nombre,
                        precioId = idPrecioGrupo,
                        estatusFacturarMayor = ent.estatus_facturar_mayor,
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

        public DtoLib.ResultadoAuto Sucursal_Agregar(DtoLibSistema.Sucursal.Agregar ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "update sistema_contadores set a_empresa_sucursal=a_empresa_sucursal+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aEmpresaSucursal = cnn.Database.SqlQuery<int>("select a_empresa_sucursal from sistema_contadores").FirstOrDefault();
                        var autoEmpresaSucursal = aEmpresaSucursal.ToString().Trim().PadLeft(10, '0');

                        var ent = cnn.empresa_sucursal.FirstOrDefault(f => f.codigo == ficha.codigo);
                        if (ent != null) 
                        {
                            result.Mensaje = "CODIGO SUCURSAL YA REGISTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        ent = new empresa_sucursal()
                        {
                            auto = autoEmpresaSucursal,
                            autoEmpresaGrupo = ficha.autoGrupo,
                            autoDepositoPrincipal = "",
                            nombre = ficha.nombre,
                            codigo = ficha.codigo,
                            estatus_facturar_mayor = ficha.estatusFactMayor,
                        };
                        cnn.empresa_sucursal.Add(ent);
                        cnn.SaveChanges();

                        ts.Complete();
                        result.Auto = autoEmpresaSucursal;
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

        public DtoLib.Resultado Sucursal_Editar(DtoLibSistema.Sucursal.Editar ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.empresa_sucursal.Find(ficha.auto);
                        if (ent == null) 
                        {
                            result.Mensaje = "[ ID ] ENTIDAD SUCURSAL NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        ent.autoEmpresaGrupo=ficha.autoGrupo;
                        ent.codigo=ficha.codigo;
                        ent.nombre = ficha.nombre;
                        ent.estatus_facturar_mayor = ficha.estatusFactMayor;
                        cnn.SaveChanges();

                        var cnt = cnn.empresa_sucursal.Where(f => f.codigo == ficha.codigo).Count();
                        if (cnt > 1) 
                        {
                            result.Mensaje = "CODIGO SUCURSAL YA REGISTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

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

        public DtoLib.Resultado Sucursal_AsignarDepositoPrincipal(DtoLibSistema.Sucursal.AsignarDepositoPrincipal ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.empresa_sucursal.Find(ficha.auto);
                        if (ent == null)
                        {
                            result.Mensaje = "[ ID ] ENTIDAD SUCURSAL NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.autoDepositoPrincipal = ficha.autoDepositoPrincipal;
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

        public DtoLib.Resultado Sucursal_QuitarDepositoPrincipal(string autoSuc)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.empresa_sucursal.Find(autoSuc);
                        if (ent == null)
                        {
                            result.Mensaje = "[ ID ] ENTIDAD SUCURSAL NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.autoDepositoPrincipal = "";
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

        public DtoLib.ResultadoEntidad<int> Sucursal_GeneraCodigoAutomatico()
        {
            var result = new DtoLib.ResultadoEntidad<int>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    int? aEmpresaSucursal = cnn.Database.SqlQuery<int?>("select a_empresa_sucursal from sistema_contadores").FirstOrDefault();
                    if (!aEmpresaSucursal.HasValue)
                    {
                        result.Mensaje = "[ AUTOMATICO EMPRESA SUCURSAL ] PROBLEMA AL CONSULTAR CAMPO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = ((int)aEmpresaSucursal.Value)+1;
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
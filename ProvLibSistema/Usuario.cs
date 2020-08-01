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

        public DtoLib.ResultadoEntidad<DtoLibSistema.Usuario.Ficha> Usuario_Principal()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Usuario.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.usuarios.Find("0000000001");

                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] USUARIO PRINCIPAL NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var nr = new DtoLibSistema.Usuario.Ficha()
                    {
                        auto = ent.auto,
                        codigo = ent.codigo,
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

        public DtoLib.ResultadoLista<DtoLibSistema.Usuario.Resumen> Usuario_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibSistema.Usuario.Resumen>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var q = cnn.usuarios.ToList();

                    var list = new List<DtoLibSistema.Usuario.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var _fecha= new DateTime(2000,01,01);
                                var _fechaBaja=s.fecha_baja==_fecha?(DateTime?)null:s.fecha_baja;
                                var _fechaUltSesion=s.fecha_sesion==_fecha?(DateTime?)null:s.fecha_sesion;
                                var _estatus=DtoLibSistema.Usuario.Enumerados.EnumModo.Activo;
                                if (s.estatus.Trim().ToUpper()!="ACTIVO")
                                {
                                    _estatus= DtoLibSistema.Usuario.Enumerados.EnumModo.Inactivo;
                                }
                                var _grupo="";
                                var entGrupo= cnn.usuarios_grupo.Find(s.auto_grupo);
                                if (entGrupo!=null)
                                {
                                    _grupo=entGrupo.nombre;
                                }
                                var r = new DtoLibSistema.Usuario.Resumen()
                                {
                                    auto = s.auto,
                                    codigo = s.codigo,
                                    nombre = s.nombre,
                                    apellido = s.apellido,
                                    fechaAlta = s.fecha_alta,
                                    fechaBaja = _fechaBaja,
                                    fechaUltSesion = _fechaUltSesion,
                                    grupo = _grupo,
                                    estatus = _estatus,
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

        public DtoLib.ResultadoEntidad<DtoLibSistema.Usuario.Ficha> Usuario_GetFicha(string autoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Usuario.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.usuarios.Find(autoUsu);
                    if (ent == null) 
                    {
                        result.Mensaje = "[ ID ] USUARIO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var _fecha = new DateTime(2000, 01, 01);
                    var _fechaBaja = ent.fecha_baja == _fecha ? (DateTime?)null : ent.fecha_baja;
                    var _fechaUltSesion = ent.fecha_sesion == _fecha ? (DateTime?)null : ent.fecha_sesion;
                    var _estatus = DtoLibSistema.Usuario.Enumerados.EnumModo.Activo;
                    if (ent.estatus.Trim().ToUpper() != "ACTIVO")
                    {
                        _estatus = DtoLibSistema.Usuario.Enumerados.EnumModo.Inactivo;
                    }
                    var _grupo = "";
                    var _autoGrupo="";
                    var entGrupo = cnn.usuarios_grupo.Find(ent.auto_grupo);
                    if (entGrupo != null)
                    {
                        _grupo = entGrupo.nombre;
                        _autoGrupo=entGrupo.auto;
                    }
                    var r = new DtoLibSistema.Usuario.Ficha()
                    {
                        auto = ent.auto,
                        codigo = ent.codigo,
                        nombre = ent.nombre,
                        apellido = ent.apellido,
                        fechaAlta = ent.fecha_alta,
                        fechaBaja = _fechaBaja,
                        fechaUltSesion = _fechaUltSesion,
                        grupo = _grupo,
                        estatus = _estatus,
                        autoGrupo=_autoGrupo,
                        clave=ent.clave,
                    };
                    result.Entidad = r;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoAuto Usuario_Agregar(DtoLibSistema.Usuario.Agregar ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var sql = "update sistema_contadores set a_usuarios=a_usuarios+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aUsuario = cnn.Database.SqlQuery<int>("select a_usuarios from sistema_contadores").FirstOrDefault();
                        var autoUsuario = aUsuario.ToString().Trim().PadLeft(10, '0');

                        var ent = new usuarios()
                        {
                            auto = autoUsuario,
                            auto_grupo = ficha.autoGrupo,
                            codigo = ficha.codigo,
                            nombre = ficha.nombre,
                            apellido = ficha.apellido,
                            clave = ficha.clave,
                            fecha_alta = fechaSistema.Date,
                            fecha_baja = new DateTime(2000, 01, 01),
                            fecha_sesion = fechaSistema.Date,
                            estatus = ficha.estatus,
                            estatus_replica = "0",
                        };
                        cnn.usuarios.Add(ent);
                        cnn.SaveChanges();

                        ts.Complete();
                        result.Auto = autoUsuario;
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

        public DtoLib.Resultado Usuario_Editar(DtoLibSistema.Usuario.Editar ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.usuarios.Find(ficha.auto);
                        if (ent == null) 
                        {
                            result.Mensaje = "[ ID ] USUARIO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        ent.auto_grupo=ficha.autoGrupo;
                        ent.codigo=ficha.codigo;
                        ent.nombre=ficha.nombre;
                        ent.apellido=ficha.apellido;
                        ent.clave=ficha.clave;
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

        public DtoLib.Resultado Usuario_Activar(DtoLibSistema.Usuario.Activar ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.usuarios.Find(ficha.auto);
                        if (ent == null)
                        {
                            result.Mensaje = "[ ID ] USUARIO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        ent.estatus = "Activo";
                        ent.fecha_baja = new DateTime(2000, 01, 01);
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

        public DtoLib.Resultado Usuario_Inactivar(DtoLibSistema.Usuario.Inactivar ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var ent = cnn.usuarios.Find(ficha.auto);
                        if (ent == null)
                        {
                            result.Mensaje = "[ ID ] USUARIO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        ent.estatus = "Inactivo";
                        ent.fecha_baja = fechaSistema.Date;
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
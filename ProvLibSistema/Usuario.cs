using LibEntitySistema;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
                        fechaBaja = ent.fecha_baja,
                        fechaUltSesion = ent.fecha_sesion,
                        grupo = _grupo,
                        estatus = ent.estatus,
                        autoGrupo = _autoGrupo,
                        clave = ent.clave,
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

        public DtoLib.ResultadoEntidad<DtoLibSistema.Usuario.Cargar.Ficha> Usuario_Buscar(DtoLibSistema.Usuario.Buscar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Usuario.Cargar.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var sql = "SELECT usu.auto as autoUsu, usu.nombre as nombreUsu , usu.apellido as apellidoUsu, " +
                        "usu.codigo as codigoUsu, usu.estatus as estatusUsu, usu.auto_grupo as autoGru, gru.nombre as nombreGru " +
                        "FROM usuarios as usu " +
                        "join usuarios_grupo as gru " +
                        "on usu.auto_grupo=gru.auto " +
                        "where usu.codigo=@p1 and usu.clave=@p2";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.codigo);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.clave);
                    var ent = cnn.Database.SqlQuery<DtoLibSistema.Usuario.Cargar.Ficha>(sql, p1, p2).FirstOrDefault();

                    if (ent == null)
                    {
                        result.Mensaje = "USUARIO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    result.Entidad = ent;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado Usuario_ActualizarSesion(DtoLibSistema.Usuario.ActualizarSesion.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var ent = cnn.usuarios.Find(ficha.autoUsu);
                        if (ent == null)
                        {
                            result.Mensaje = "[ ID ] USUARIO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        ent.fecha_sesion = fechaSistema.Date;
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

        public DtoLib.ResultadoLista<DtoLibSistema.Usuario.Resumen> Usuario_GetLista(DtoLibSistema.Usuario.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibSistema.Usuario.Resumen>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql_1 = @"select u.auto as uId, u.nombre as uNombre, u.apellido uApellido, 
                                    u.codigo as uCodigo, u.estatus as uEstatus, 
                                    u.fecha_alta as uFechaAlta, u.fecha_baja as uFechaBaja, 
                                    u.fecha_sesion as uFechaUltSesion, 
                                    ug.nombre as gNombre
                                    from usuarios as u join usuarios_grupo as ug 
                                    on u.auto_grupo=ug.auto ";
                    var sql_2 = " where 1=1 ";
                    if (filtro.IdGrupo!="")
                    {
                        p1.ParameterName = "@idGrupo";
                        p1.Value = filtro.IdGrupo;
                        sql_2 += " and u.auto_grupo=@idGrupo ";
                    }
                    var sql=sql_1+sql_2;
                    var lst = cnn.Database.SqlQuery<DtoLibSistema.Usuario.Resumen>(sql, p1).ToList(); ;
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

        public DtoLib.Resultado Usuario_Eliminar(string id)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", id);
                        var sql_1 = "delete from usuarios where auto=@id";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql_1, p1);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL TRATAR DE ELIMINAR USUARIO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                if (ex.Number == 1451)
                {
                    result.Mensaje = "REGISTRO CONTIENE DATA RELACIONADA";
                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                    return result;
                }
                result.Mensaje = ex.Message;
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
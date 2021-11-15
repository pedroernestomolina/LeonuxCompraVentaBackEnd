using LibEntitySistema;
using MySql.Data.MySqlClient;
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

        public DtoLib.ResultadoLista<DtoLibSistema.GrupoUsuario.Resumen> GrupoUsuario_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibSistema.GrupoUsuario.Resumen>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var q = cnn.usuarios_grupo.ToList();

                    var list = new List<DtoLibSistema.GrupoUsuario.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var r = new DtoLibSistema.GrupoUsuario.Resumen()
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

        public DtoLib.ResultadoEntidad<DtoLibSistema.GrupoUsuario.Ficha> GrupoUsuario_GetFicha(string auto)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.GrupoUsuario.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.usuarios_grupo.Find(auto);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] ENTIDAD GRUPO USUARIO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var nr = new DtoLibSistema.GrupoUsuario.Ficha()
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

        public DtoLib.ResultadoAuto GrupoUsuario_Agregar(DtoLibSistema.GrupoUsuario.Agregar ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "update sistema_contadores set a_usuarios_grupo=a_usuarios_grupo+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aUsuarioGrupo = cnn.Database.SqlQuery<int>("select a_usuarios_grupo from sistema_contadores").FirstOrDefault();
                        var autoUsuarioGrupo = aUsuarioGrupo.ToString().Trim().PadLeft(10, '0');

                        var ent = new usuarios_grupo()
                        {
                            auto = autoUsuarioGrupo,
                            nombre = ficha.nombre,
                        };
                        cnn.usuarios_grupo .Add(ent);
                        cnn.SaveChanges();

                        sql = @"INSERT INTO usuarios_grupo_permisos (codigo_grupo,codigo_funcion,estatus,seguridad) " +
                            "VALUES (@p1, @p2, @p3,@p4)";
                        var p1 = new MySqlParameter();
                        var p2 = new MySqlParameter();
                        var p3 = new MySqlParameter();
                        var p4 = new MySqlParameter();
                        foreach (var it in ficha.permisos) 
                        {
                            p1.ParameterName="@p1";
                            p1.Value = autoUsuarioGrupo;
                            p2.ParameterName = "@p2";
                            p2.Value = it.codigoFuncion;
                            p3.ParameterName = "@p3";
                            p3.Value = it.estatus;
                            p4.ParameterName = "@p4";
                            p4.Value = it.seguridad;

                            r1 = cnn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4);
                            if (r1 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL INSERTAR PERMISOS AL GRUPO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cnn.SaveChanges();
                        }

                        ts.Complete();
                        result.Auto = autoUsuarioGrupo ;
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

        public DtoLib.Resultado GrupoUsuario_Editar(DtoLibSistema.GrupoUsuario.Editar ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.usuarios_grupo.Find(ficha.auto);
                        if (ent == null)
                        {
                            result.Mensaje = "[ ID ] ENTIDAD GRUPO USUARIO NO ENCONTRADO";
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

        public DtoLib.Resultado GrupoUsuario_ELiminar(string auto)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoGrupo", auto);

                        var sql_1 = "delete from usuarios_grupo where auto=@autoGrupo";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql_1, p1);
                        if (r1 == 0) 
                        {
                            result.Mensaje = "PROBLEMA AL TRATAR DE ELIMINAR USUARIO GRUPO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var sql_2 = "delete from usuarios_grupo_permisos where codigo_grupo=@autoGrupo";
                        var r2 = cnn.Database.ExecuteSqlCommand(sql_2, p1);
                        if (r2 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL TRATAR DE ELIMINAR PERMISOS DEL USUARIO GRUPO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                       
                        cnn.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                var dbUpdateEx = ex as DbUpdateException;
                var sqlEx = dbUpdateEx.InnerException;
                if (sqlEx != null)
                {
                    var exx = (MySql.Data.MySqlClient.MySqlException)sqlEx.InnerException;
                    if (exx != null)
                    {
                        if (exx.Number == 1451)
                        {
                            result.Mensaje = "REGISTRO CONTIENE DATA RELACIONADA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                    }
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

        public DtoLib.ResultadoLista<DtoLibSistema.GrupoUsuario.Usuario> GrupoUsuario_GetUsuarios(string auto)
        {
            var result = new DtoLib.ResultadoLista<DtoLibSistema.GrupoUsuario.Usuario>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1= new MySql.Data.MySqlClient.MySqlParameter("@autoGrupo", auto);
                    var sql = @"select auto as autoId, nombre, apellido, codigo, estatus 
                                from usuarios where auto_grupo=@autoGrupo";
                    var lst = cnn.Database.SqlQuery<DtoLibSistema.GrupoUsuario.Usuario>(sql, p1).ToList();
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

        public DtoLib.Resultado GrupoUsuario_Validar_EliminarGrupo(string auto)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var q = cnn.usuarios.Where(w=>w.auto_grupo==auto).ToList();
                    if (q.Count >0)
                    {
                        result.Mensaje = "HAY USUARIOS RELACIONADOS A ESTE GRUPO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
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
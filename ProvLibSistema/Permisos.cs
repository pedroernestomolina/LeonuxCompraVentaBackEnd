using LibEntitySistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibSistema
{

    public partial class Provider : ILibSistema.IProvider
    {

        public DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMaximo()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL17");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    result.Entidad = ent.usuario.Trim().ToUpper();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMedio()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL18");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    result.Entidad = ent.usuario.Trim().ToUpper();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMinimo()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL19");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    result.Entidad = ent.usuario.Trim().ToUpper();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ToolSistema(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery< DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1220000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_InicializarBD(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1221000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_InicializarBD_Sucursal(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1222000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_AjustarTasaDivisa(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1223000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_AjustarTasaDivisaRecepcionPos(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1224000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_EtiquetaParaPrecios(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1225000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_AsignarDepositoSucursal(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1226000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_AsignarDepositoSucursal_EliminarAsignacion(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1226010000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_AsignarDepositoSucursal_EditarAsignacion(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1226020000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSucursalGrupo(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1227000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSucursalGrupo_Agregar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1227010000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSucursalGrupo_Editar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1227020000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSucursal(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1228000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSucursal_Agregar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1228010000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSucursal_Editar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1228020000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlDeposito(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1229000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlDeposito_Agregar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1229010000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlDeposito_Editar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1229020000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlDeposito_ActivarInactivar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var sql = @"select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1229030000'";
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuarioGrupo(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1230000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuarioGrupo_Agregar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1230010000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuarioGrupo_Editar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1230020000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuarioGrupo_Eliminar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var sql = @"select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1230030000'";
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuario(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1204000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuario_Agregar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1204010000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuario_Editar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1204020000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuario_ActivarInactivar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1204040000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlUsuario_Eliminar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1204030000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlVendedor(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1240000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlVendedor_Agregar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1240010000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlVendedor_Editar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1240020000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlVendedor_ActivarInactivar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1240030000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlCobrador(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1250000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlCobrador_Agregar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1250010000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlCobrador_Editar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1250020000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSerieFiscal(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1260000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSerieFiscal_Agregar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1260010000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSerieFiscal_Editar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='12600200000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_ControlSerieFiscal_ActivarInactivar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1260030000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha> Permiso_Configuracion_Sistema(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Permiso.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibSistema.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1202000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
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
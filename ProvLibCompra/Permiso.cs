using LibEntityCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibCompra
{
    
    public partial class Provider: ILibCompras.IProvider
    {

        public DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMaximo()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
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
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
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
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
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


        public DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_ToolCompra(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibCompra.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0720000000'", p1).FirstOrDefault();
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


        public DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Registrar_Factura(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibCompra.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0701000000'", p1).FirstOrDefault();
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

        public DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Registrar_Nc(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibCompra.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0704000000'", p1).FirstOrDefault();
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


        public DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_AdmDoc(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibCompra.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0705000000'", p1).FirstOrDefault();
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

        public DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_AdmDoc_Anular(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibCompra.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0705010000'", p1).FirstOrDefault();
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

        public DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_AdmDoc_Visualizar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibCompra.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0705020000'", p1).FirstOrDefault();
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

        public DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_AdmDoc_Reporte(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibCompra.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0705030000'", p1).FirstOrDefault();
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

        public DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_AdmDoc_Corrector(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibCompra.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0705040000'", p1).FirstOrDefault();
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

        public DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Reportes(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibCompra.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0799000000'", p1).FirstOrDefault();
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


        public DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Grupo(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibCompra.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0202000000'", p1).FirstOrDefault();
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
        public DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_CrearGrupo(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibCompra.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0202010000'", p1).FirstOrDefault();
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
        public DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_ModificarGrupo(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibCompra.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0202020000'", p1).FirstOrDefault();
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

        public DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Proveedor(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibCompra.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0201000000'", p1).FirstOrDefault();
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
        public DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Proveedor_Agregar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibCompra.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0201010000'", p1).FirstOrDefault();
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
        public DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Proveedor_Editar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibCompra.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0201020000'", p1).FirstOrDefault();
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
        public DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Proveedor_CambiarEstatus(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibCompra.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0201040000'", p1).FirstOrDefault();
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

        public DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha> Permiso_Proveedor_Reportes(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Permiso.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibCompra.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0299000000'", p1).FirstOrDefault();
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
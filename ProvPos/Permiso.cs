using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{
    
    public partial class Provider: IPos.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_IngresarPos(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0816000000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
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

        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Pos(string idGrupoUsu, string codFuncion)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion=@p2";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    p2.ParameterName = "@p2";
                    p2.Value = codFuncion;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1,p2).FirstOrDefault();
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
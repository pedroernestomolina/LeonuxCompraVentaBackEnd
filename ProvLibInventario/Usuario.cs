using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{
    
    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibInventario.Usuario.Ficha> Usuario_Principal()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Usuario.Ficha >();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = "SELECT usu.auto as autoUsu, usu.nombre as nombreUsu , usu.apellido as apellidoUsu, "+
                        "usu.codigo as codigoUsu, usu.estatus as estatusUsu, usu.auto_grupo as autoGru, gru.nombre as nombreGru " +
                        "FROM usuarios as usu " +
                        "join usuarios_grupo as gru " +
                        "on usu.auto_grupo=gru.auto " +
                        "where usu.auto='0000000001'";
                    var ent = cnn.Database.SqlQuery<DtoLibInventario.Usuario.Ficha>(sql).FirstOrDefault();

                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] USUARIO PRINCIPAL NO ENCONTRADO";
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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Usuario.Ficha> Usuario_Buscar(DtoLibInventario.Usuario.Buscar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Usuario.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = "SELECT usu.auto as autoUsu, usu.nombre as nombreUsu , usu.apellido as apellidoUsu, " +
                        "usu.codigo as codigoUsu, usu.estatus as estatusUsu, usu.auto_grupo as autoGru, gru.nombre as nombreGru " +
                        "FROM usuarios as usu " +
                        "join usuarios_grupo as gru " +
                        "on usu.auto_grupo=gru.auto " +
                        "where usu.codigo=@p1 and usu.clave=@p2";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.codigo);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.clave);
                    var ent = cnn.Database.SqlQuery<DtoLibInventario.Usuario.Ficha>(sql,p1,p2).FirstOrDefault();

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

    }

}
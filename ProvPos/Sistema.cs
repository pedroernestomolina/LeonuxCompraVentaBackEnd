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

        public DtoLib.ResultadoEntidad<DtoLibPos.Sistema.TipoDocumento.Entidad.Ficha> Sistema_TipoDocumento_GetFichaById(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Sistema.TipoDocumento.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@p1";
                    p1.Value = id;
                    var sql = @"SELECT auto as autoId, tipo, codigo, nombre, signo, siglas
                                FROM sistema_documentos 
                                WHERE auto=@p1";
                    var ent = cnn.Database.SqlQuery<DtoLibPos.Sistema.TipoDocumento.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID DOCUMENTO ] NO ENCONTRADO";
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

        public DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Serie.Entidad.Ficha> Sistema_Serie_GetFichaById(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Serie.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@p1";
                    p1.Value = id;
                    var sql = @"SELECT auto, serie, control 
                                FROM empresa_series_fiscales 
                                WHERE auto=@p1";
                    var ent = cnn.Database.SqlQuery<DtoLibPos.Sistema.Serie.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID SERIE ] NO ENCONTRADO";
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
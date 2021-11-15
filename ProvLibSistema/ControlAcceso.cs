using LibEntitySistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibSistema
{

    public partial class Provider : ILibSistema.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibSistema.ControlAcceso.Data.Ficha> ControlAcceso_GetData(string idGrupo)
        {
            var result = new DtoLib.ResultadoLista<DtoLibSistema.ControlAcceso.Data.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idGrupo", idGrupo);
                    var sql = @"SELECT ug.estatus, ug.seguridad, sf.codigo as fCodigo, sf.nombre as fNombre 
                                from usuarios_grupo_permisos as ug 
                                join sistema_funciones as sf on ug.codigo_funcion=sf.codigo
                                join sistema_funciones_ext as sfe on sf.codigo=sfe.codigo
                                where ug.codigo_grupo=@idGrupo and sfe.estatus='1'";
                    var lst = cnn.Database.SqlQuery<DtoLibSistema.ControlAcceso.Data.Ficha>(sql, p1).ToList();
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

        public DtoLib.Resultado ControlAcceso_Actualizar(DtoLibSistema.ControlAcceso.Actualizar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        foreach (var it in ficha.ItemsAcceso)
                        {
                            var p1 = new MySql.Data.MySqlClient.MySqlParameter("@estatus", it.estatus);
                            var p2 = new MySql.Data.MySqlClient.MySqlParameter("@seguridad", it.seguridad);
                            var p3 = new MySql.Data.MySqlClient.MySqlParameter("@grupo", it.codGrupo);
                            var p4 = new MySql.Data.MySqlClient.MySqlParameter("@funcion", it.codFuncion);
                            var sql_1 = @"update usuarios_grupo_permisos
                                        set estatus=@estatus, seguridad=@seguridad
                                        where codigo_grupo=@grupo and codigo_funcion=@funcion";
                            var r1 = cnn.Database.ExecuteSqlCommand(sql_1, p1, p2, p3, p4);
                            if (r1 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR PERMISO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cnn.SaveChanges();
                        }
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
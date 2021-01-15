using LibEntityCompra;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibCompra
{
    
    public partial class Provider: ILibCompras.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibCompra.Usuario.Data.Ficha> Usuario_Principal()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Usuario.Data.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var sql = "SELECT usu.auto as autoUsu, usu.nombre as nombreUsu , usu.apellido as apellidoUsu, " +
                        "usu.codigo as codigoUsu, usu.estatus as estatusUsu, usu.auto_grupo as autoGru, gru.nombre as nombreGru " +
                        "FROM usuarios as usu " +
                        "join usuarios_grupo as gru " +
                        "on usu.auto_grupo=gru.auto " +
                        "where usu.auto='0000000001'";
                    var ent = cnn.Database.SqlQuery<DtoLibCompra.Usuario.Data.Ficha>(sql).FirstOrDefault();

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

        public DtoLib.ResultadoEntidad<DtoLibCompra.Usuario.Data.Ficha> Usuario_Buscar(DtoLibCompra.Usuario.Buscar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Usuario.Data.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var sql = "SELECT usu.auto as autoUsu, usu.nombre as nombreUsu , usu.apellido as apellidoUsu, " +
                        "usu.codigo as codigoUsu, usu.estatus as estatusUsu, usu.auto_grupo as autoGru, gru.nombre as nombreGru " +
                        "FROM usuarios as usu " +
                        "join usuarios_grupo as gru " +
                        "on usu.auto_grupo=gru.auto " +
                        "where usu.codigo=@p1 and usu.clave=@p2";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.codigo);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.clave);
                    var ent = cnn.Database.SqlQuery<DtoLibCompra.Usuario.Data.Ficha>(sql, p1, p2).FirstOrDefault();

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

        public DtoLib.Resultado Usuario_ActualizarSesion(DtoLibCompra.Usuario.ActualizarSesion.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
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

    }

}
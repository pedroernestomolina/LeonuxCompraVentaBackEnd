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

        public DtoLib.Resultado TablaPrecio_Crear(DtoLibSistema.TablaPrecio.Crear.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@codigo", ficha.codigo);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@descripcion", ficha.descripcion);
                        var sql_1 = @"INSERT INTO empresa_hnd_precios (id, codigo, descrpcion, estatus) 
                                    VALUES (NULL, @codigo, @descripcion, '1')";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql_1, p1, p2);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL GRABAR REGISTRO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.SaveChanges();

                        var sql_2 = "select @@identity";
                        var r2 = cnn.Database.SqlQuery<int>(sql_2).FirstOrDefault();
                        if (r2 == null)
                        {
                            result.Mensaje = "PROBLEMA AL GRABAR REGISTRO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var sql_3 = @"INSERT INTO productos_hnd_precios (
                                    id,
                                    idEmpresaPrecio,
                                    idProducto,
                                    idMedida_1,
                                    idMedida_2,
                                    contenido_1,
                                    neto_1,
                                    netoDivisa_1,
                                    utilidad_1,
                                    contenido_2,
                                    neto_2,
                                    netoDivisa_2,
                                    utilidad_2) 
                                    select NULL, @idPrecio, auto, '0000000001', '0000000001', 1, 0, 0, 0, 1, 0, 0, 0 
                                        from productos";
                        var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@idPrecio", r2);
                        var r3 = cnn.Database.ExecuteSqlCommand(sql_3, xp1);
                        if (r3 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL EGISTRAR PRECIOS";
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
                if (ex.Number == 1062)
                {
                    result.Mensaje = "CAMPO DUPLICADO" + Environment.NewLine + ex.Message;
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
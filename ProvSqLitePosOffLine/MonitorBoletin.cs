using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{

    public partial class Provider : IPosOffLine.IProvider
    {

        public DtoLib.Resultado MonitorBoletin_Actualizar(string boletin)
        {
            var result = new DtoLib.Resultado();

            try
            {

                using (var cn = new MySqlConnection(_cnn3.ConnectionString))
                {
                    cn.Open();
                    MySqlTransaction tr = null;

                    try
                    {
                        tr = cn.BeginTransaction();

                        var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                        p1.ParameterName = "boletin";
                        p1.Value = boletin;
                        var sql1 = @"update monitor_boletin set boletin_info=@boletin where id=1";
                        var comando1 = new MySqlCommand(sql1, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.Add(p1);
                        var idObj = comando1.ExecuteScalar();

                        tr.Commit();
                    }
                    catch (Exception ex1)
                    {
                        tr.Rollback();
                        result.Mensaje = ex1.Message;
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                    }
                };
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<string> MonitorBoletin_Info()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cn = new MySqlConnection(_cnn3.ConnectionString))
                {
                    cn.Open();

                    try
                    {
                        var sql1 = @"select boletin_info from monitor_boletin where id=1";
                        var comando1 = new MySqlCommand(sql1, cn);
                        var idObj = comando1.ExecuteScalar();
                        if (idObj != null) 
                        {
                            result.Entidad = idObj.ToString();
                        }
                    }
                    catch (Exception ex1)
                    {
                        result.Mensaje = ex1.Message;
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                    }
                };
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
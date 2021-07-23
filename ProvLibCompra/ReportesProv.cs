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

        public DtoLib.ResultadoLista<DtoLibCompra.Reportes.Proveedor.Maestro.Ficha> ReportesProv_Maestro(DtoLibCompra.Reportes.Proveedor.Maestro.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCompra.Reportes.Proveedor.Maestro.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = @"SELECT 
                        codigo as codigo,   
                        ci_rif as ciRif,
                        razon_social as nombre,
                        dir_fiscal as dirFiscal,
                        telefono as telefono,
                        estatus ";

                    var sql_2 = @" FROM proveedores ";

                    var sql_3 = "where 1=1 ";

                    var sql_4 = "";

                    if (filtro.idGrupo != "")
                    {
                        sql_3 += " and auto_grupo=@idGrupo";
                        p1.ParameterName = "@idGrupo";
                        p1.Value = filtro.idGrupo;
                    }
                    if (filtro.idEstado != "")
                    {
                        sql_3 += " and auto_estado=@idEstado";
                        p2.ParameterName = "@idEstado";
                        p2.Value = filtro.idEstado;
                    }
                    if (filtro.estatus != "")
                    {
                        sql_3 += " and estatus=@estatus";
                        p3.ParameterName = "@estatus";
                        p3.Value = filtro.estatus;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibCompra.Reportes.Proveedor.Maestro.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

    }

}
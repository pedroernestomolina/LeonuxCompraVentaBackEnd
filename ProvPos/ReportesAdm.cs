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

        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumento.Ficha> ReportesAdm_GeneralDocumento(DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumento.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumento.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = @"SELECT 
                        v.fecha, 
                        v.documento,
                        v.control, 
                        v.serie, 
                        v.estatus_anulado as estatusDoc, 
                        v.razon_social as clienteNombre, 
                        v.ci_rif as clienteCiRif, 
                        v.total, 
                        v.tipo as tipoDoc, 
                        v.monto_divisa as totalDivisa, 
                        v.renglones, 
                        v.factor_cambio as factorDoc, 
                        v.signo as signoDoc, 
                        v.documento_nombre as nombreDoc, 
                        (v.descuento1+v.descuento2) as montoDscto, 
                        v.cargos as montoCargo ";

                    var sql_2 = "FROM ventas as v ";

                    var sql_3 = "where 1=1 ";

                    var sql_4 = "";

                    sql_3 += " and v.fecha>=@desde ";
                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;

                    sql_3 += " and v.fecha<=@hasta ";
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;

                    if (filtro.codSucursal != "")
                    {
                        sql_3 += " and v.codigo_sucursal=@suc ";
                        p3.ParameterName = "@suc";
                        p3.Value = filtro.codSucursal;
                    }
                    if (filtro.tipoDocFactura)
                    {
                        sql_3 += " and v.tipo='01' ";
                    }
                    if (filtro.tipoDocNtDebito)
                    {
                        sql_3 += " and v.tipo='02' ";
                    }
                    if (filtro.tipoDocNtCredito)
                    {
                        sql_3 += " and v.tipo='03' ";
                    }
                    if (filtro.tipoDocNtEntrega)
                    {
                        sql_3 += " and v.tipo='04' ";
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumento.Ficha>(sql, p1, p2, p3, p4).ToList();
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
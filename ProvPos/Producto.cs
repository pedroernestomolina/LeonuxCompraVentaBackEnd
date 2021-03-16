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

        public DtoLib.ResultadoLista<DtoLibPos.Producto.Lista.Ficha> Producto_GetLista(DtoLibPos.Producto.Lista.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Producto.Lista.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = " select p.auto, p.codigo, p.nombre, p.estatus, p.estatus_divisa as estatusDivisa, "+
                        "p.estatus_pesado as estatusPesado , p.tasa as tasaIva, " +
                        "pd.fisica as exFisica, pd.disponible as exDisponible ";

                    var sql_2 = " from productos as p " +
                        " join productos_deposito as pd on p.auto=pd.auto_producto ";

                    var sql_3 = " where 1=1 ";
                    var sql_4 = "";

                    if (filtro.Cadena.Trim() != "") 
                    {
                        var cad = filtro.Cadena.Trim();
                        sql_3 += " and p.nombre like @p1 ";
                        p1.ParameterName = "@p1";
                        p1.Value = cad + "%";
                    }

                    if (filtro.AutoDeposito.Trim() != "")
                    {
                        sql_3 += " and pd.auto_deposito=@p2 ";
                        p2.ParameterName = "@p2";
                        p2.Value = filtro.AutoDeposito;
                    }

                    if (filtro.IdPrecioManejar.Trim() != "")
                    {
                        var idPrecio=filtro.IdPrecioManejar.Trim();
                        switch (idPrecio) 
                        {
                            case "1":
                                sql_1 += " ,p.precio_1 as precioNeto, p.pdf_1 as precioFullDivisa, "+
                                    "p.contenido_1 as contenido, pm.decimales, pm.nombre as empaque ";
                                sql_2 += " join productos_medida as pm on p.auto_precio_1=pm.auto ";
                                break;
                            case "2":
                                sql_1 += " ,p.precio_2 as precioNeto, p.pdf_2 as precioFullDivisa, " +
                                    "p.contenido_2 as contenido, pm.decimales, pm.nombre as empaque ";
                                sql_2 += " join productos_medida as pm on p.auto_precio_2=pm.auto ";
                                break;
                            case "3":
                                sql_1 += " ,p.precio_3 as precioNeto, p.pdf_3 as precioFullDivisa, " +
                                    "p.contenido_3 as contenido, pm.decimales, pm.nombre as empaque ";
                                sql_2 += " join productos_medida as pm on p.auto_precio_3=pm.auto ";
                                break;
                            case "4":
                                sql_1 += " ,p.precio_4 as precioNeto, p.pdf_4 as precioFullDivisa, " +
                                    "p.contenido_4 as contenido, pm.decimales, pm.nombre as empaque ";
                                sql_2 += " join productos_medida as pm on p.auto_precio_4=pm.auto ";
                                break;
                            case "5":
                                sql_1 += " ,p.precio_pto as precioNeto, p.pdf_pto as precioFullDivisa, " +
                                    "p.contenido_pto as contenido, pm.decimales, pm.nombre as empaque ";
                                sql_2 += " join productos_medida as pm on p.auto_precio_pto=pm.auto ";
                                break;
                        }
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var q = cnn.Database.SqlQuery<DtoLibPos.Producto.Lista.Ficha>(sql,p1,p2,p3).ToList();
                    rt.Lista = q;
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
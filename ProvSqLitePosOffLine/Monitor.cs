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

        public DtoLib.Resultado Monitor_SubirResumen(DtoLibPosOffLine.Monitor.SubirResumen.Ficha ficha)
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

                        var p10 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p11 = new MySql.Data.MySqlClient.MySqlParameter();
                        p10.ParameterName = "suc";
                        p10.Value = ficha.codSucursal;
                        p11.ParameterName = "cierre";
                        p11.Value = ficha.cierre;
                        var sql1 = @"select id from monitor_resumen where codSucursal=@suc and cierre=@cierre";
                        var comando1 = new MySqlCommand(sql1, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.Add(p10);
                        comando1.Parameters.Add(p11);
                        var idObj = comando1.ExecuteScalar();

                        if (idObj != null)
                        {
                            sql1 = @"delete from monitor_resumen_det where idResumen=@id";
                            comando1 = new MySqlCommand(sql1, cn, tr);
                            var p12 = new MySql.Data.MySqlClient.MySqlParameter();
                            p12.ParameterName = "id";
                            p12.Value = idObj;
                            comando1.Parameters.Clear();
                            comando1.Parameters.Add(p12);
                            comando1.ExecuteNonQuery();

                            sql1 = @"delete from monitor_resumen where id=@id";
                            comando1 = new MySqlCommand(sql1, cn, tr);
                            comando1.Parameters.Clear();
                            comando1.Parameters.Add(p12);
                            comando1.ExecuteNonQuery();
                        }

                        var sql2 = @"INSERT INTO monitor_resumen (codSucursal, cierre) VALUES (@suc, @cierre)";
                        var p21 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p22 = new MySql.Data.MySqlClient.MySqlParameter();
                        p21.ParameterName = "suc";
                        p21.Value = ficha.codSucursal;
                        p22.ParameterName = "cierre";
                        p22.Value = ficha.cierre;
                        var comando2 = new MySqlCommand(sql2, cn, tr);
                        comando2.Parameters.Clear();
                        comando2.Parameters.Add(p21);
                        comando2.Parameters.Add(p22);
                        comando2.ExecuteNonQuery();

                        var sql3 = "SELECT LAST_INSERT_ID()";
                        var comando3 = new MySqlCommand(sql3, cn, tr);
                        var idR = comando3.ExecuteScalar();

                        var sql4 = @"INSERT INTO monitor_resumen_det (idResumen, autoProducto, cnt) VALUES (@idR, @autoPrd, @cnt)";
                        var comando4 = new MySqlCommand(sql4, cn, tr);
                        var p41 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p42 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p43 = new MySql.Data.MySqlClient.MySqlParameter();
                        foreach (var it in ficha.Lista)
                        {
                            p41.ParameterName = "idR";
                            p41.Value = idR;
                            p42.ParameterName = "autoPrd";
                            p42.Value = it.autoProducto;
                            p43.ParameterName = "cnt";
                            p43.Value = it.cnt;
                            comando4.Parameters.Clear();
                            comando4.Parameters.Add(p41);
                            comando4.Parameters.Add(p42);
                            comando4.Parameters.Add(p43);
                            comando4.ExecuteNonQuery();
                        }
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

        public DtoLib.ResultadoLista<DtoLibPosOffLine.Monitor.GenerarResumen.Ficha> Monitor_GenerarResumen(DtoLibPosOffLine.Monitor.GenerarResumen.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPosOffLine.Monitor.GenerarResumen.Ficha>();

            try
            {
                using (var cn = new MySqlConnection(_cnn2.ConnectionString))
                {
                    cn.Open();

                    var lst = new List<DtoLibPosOffLine.Monitor.GenerarResumen.Ficha>();
                    if (filtro.cierre != "")
                    {
                        var p0 = new MySql.Data.MySqlClient.MySqlParameter();
                        p0.ParameterName = "cierre";
                        p0.Value = filtro.cierre;
                        var sql0 = @"SELECT vd.auto_producto AS autoProducto, SUM( vd.cantidad_und * vd.signo ) AS cnt
                                FROM ventas AS v
                                JOIN ventas_detalle AS vd ON v.auto = vd.auto_documento
                                WHERE v.estatus_anulado = '0'
                                AND SUBSTR( cierre, 5 ) = @cierre
                                GROUP BY vd.auto_producto";
                        var comando1 = new MySqlCommand(sql0, cn);
                        comando1.Parameters.Clear();
                        comando1.Parameters.Add(p0);
                        var rd = comando1.ExecuteReader();
                        while (rd.Read())
                        {
                            var nr = new DtoLibPosOffLine.Monitor.GenerarResumen.Ficha()
                            {
                                autoProducto = rd.GetString("autoProducto"),
                                cnt = rd.GetDecimal("cnt"),
                            };
                            lst.Add(nr);
                        }
                        rd.Close();
                    }
                    else 
                    {
                        var sql0 = @"SELECT autoProducto, SUM(cnt) AS cnt
                                FROM monitor_venta_resumen 
                                GROUP BY autoProducto";
                        var comando1 = new MySqlCommand(sql0, cn);
                        var rd = comando1.ExecuteReader();
                        while (rd.Read())
                        {
                            var nr = new DtoLibPosOffLine.Monitor.GenerarResumen.Ficha()
                            {
                                autoProducto = rd.GetString("autoProducto"),
                                cnt = rd.GetDecimal("cnt"),
                            };
                            lst.Add(nr);
                        }
                        rd.Close();
                    }
                    result.Lista = lst;
                };
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado Monitor_InsertarCierre(DtoLibPosOffLine.Monitor.InsertarCierre.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cn = new MySqlConnection(_cnn2.ConnectionString))
                {
                    cn.Open();
                    MySqlTransaction tr = null;

                    try
                    {
                        tr = cn.BeginTransaction();

                        var p0 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                        p0.ParameterName = "cierre";
                        p0.Value = ficha.cierre;
                        p1.ParameterName = "estatus";
                        p1.Value = ficha.estatus;
                        var sql0 = @"INSERT INTO monitor_cierre (cierre, estatus) VALUES (@cierre,@estatus)";
                        var comando1 = new MySqlCommand(sql0, cn,tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.Add(p0);
                        comando1.Parameters.Add(p1);
                        comando1.ExecuteNonQuery();

                        tr.Commit();
                    }
                    catch (Exception ex1)
                    {
                        tr.Rollback();
                        result.Mensaje = ex1.Message;
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                    };
                };
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoLista<DtoLibPosOffLine.Monitor.ListaResumen.Ficha> Monitor_ListaResumen()
        {
            var result = new DtoLib.ResultadoLista<DtoLibPosOffLine.Monitor.ListaResumen.Ficha>();

            try
            {
                using (var cn = new MySqlConnection(_cnn2.ConnectionString))
                {
                    cn.Open();

                    var sql0 = @"SELECT MAX(cierre ) as ultCierre
                                FROM monitor_cierre";
                    var comando0 = new MySqlCommand(sql0, cn);
                    var ultCierre= comando0.ExecuteScalar(); 

                    var lst = new List<DtoLibPosOffLine.Monitor.ListaResumen.Ficha>();
                    var p0 = new MySql.Data.MySqlClient.MySqlParameter();
                    p0.ParameterName = "ultCierreTransmitido";
                    p0.Value = ultCierre;
                    var sql1 = @"select substr(auto_cierre,5) as cierre 
                                from pos_arqueo 
                                where substr(auto_cierre,5)>@ultCierreTransmitido";
                    var comando1 = new MySqlCommand(sql1, cn);
                    comando1.Parameters.Clear();
                    comando1.Parameters.Add(p0);
                    var rd = comando1.ExecuteReader();
                    while (rd.Read())
                    {
                        var nr = new DtoLibPosOffLine.Monitor.ListaResumen.Ficha()
                        {
                            cierreGenerar = rd.GetString("cierre"),
                        };
                        lst.Add(nr);
                    }
                    rd.Close();
                    result.Lista = lst;
                };
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        class dataDia
        {
            public string autoProducto { get; set; }
            public decimal cnt { get; set; }


            public dataDia() 
            {
                autoProducto = "";
                cnt = 0.0m;
            }
        }
        public DtoLib.Resultado Monitor_Resumen_Dia(DtoLibPosOffLine.Monitor.ResumenDia.Filtro filtro)
        {
            var result = new DtoLib.Resultado();

            try
            {
                var ldata = new List<dataDia>();
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var p1 = new System.Data.SQLite.SQLiteParameter("idOperador", filtro.idOperador);
                    var sql = @"select vd.autoProducto, sum(vd.cantidadUnd*v.signo) as cnt
                                            from VentaDetalle as vd
                                            join Venta as v on vd.idVenta=v.id
                                            where v.estatusActivo='1' and v.idOperador=@idOperador
                                            group by vd.autoProducto";
                    ldata = cnn.Database.SqlQuery<dataDia>(sql, p1).ToList();
                }
                if (ldata.Count > 0)
                {

                    using (var cn = new MySqlConnection(_cnn2.ConnectionString))
                    {
                        cn.Open();
                        MySqlTransaction tr = null;

                        try
                        {
                            tr = cn.BeginTransaction();

                            var p0 = new MySql.Data.MySqlClient.MySqlParameter();
                            p0.ParameterName = "equipo";
                            p0.Value = filtro.equipo;
                            var sql0 = @"delete from monitor_venta_resumen where equipo=@equipo";
                            var comando1 = new MySqlCommand(sql0, cn, tr);
                            comando1.Parameters.Clear();
                            comando1.Parameters.Add(p0);
                            comando1.ExecuteNonQuery();

                            sql0 = @"INSERT INTO monitor_venta_resumen (equipo, autoProducto, cnt) VALUES (@equipo, @autoPrd, @cnt)";
                            var comando2 = new MySqlCommand(sql0, cn, tr);
                            var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                            var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                            var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                            foreach (var dt in ldata)
                            {
                                p1.ParameterName = "equipo";
                                p1.Value = filtro.equipo;
                                p2.ParameterName = "autoPrd";
                                p2.Value = dt.autoProducto;
                                p3.ParameterName = "cnt";
                                p3.Value = dt.cnt;
                                comando2.Parameters.Clear();
                                comando2.Parameters.Add(p1);
                                comando2.Parameters.Add(p2);
                                comando2.Parameters.Add(p3);
                                comando2.ExecuteNonQuery();
                            }
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
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }


        //public DtoLib.ResultadoLista<DtoLibPosOffLine.ResumenVentaPos.Entidad.Ficha> VentaDocumento_Resumen_GetLista(DtoLibPosOffLine.ResumenVentaPos.Lista.Filtro filtro)
        //{
        //    throw new NotImplementedException();
        //}

        //        public DtoLib.ResultadoLista<DtoLibPosOffLine.Monitor.GenerarResumen.Ficha> Monitor_GenerarResumen(DtoLibPosOffLine.Monitor.GenerarResumen.Filtro filtro)
        //        {
        //            var result = new DtoLib.ResultadoLista<DtoLibPosOffLine.Monitor.GenerarResumen.Ficha>();

        //            try
        //            {
        //                var ldata = new List<data>();
        //                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
        //                {
        //                    var p1 = new System.Data.SQLite.SQLiteParameter("idOperador", filtro.idOperador);
        //                    var sql = @"select vd.autoProducto, sum(vd.cantidadUnd*v.signo) as cnt, v.prefijo as sucEquipo
        //                                from VentaDetalle as vd
        //                                join Venta as v on vd.idVenta=v.id
        //                                where v.estatusActivo='1' and v.idOperador=@idOperador
        //                                group by vd.autoProducto, v.prefijo";
        //                    ldata = cnn.Database.SqlQuery<data>(sql, p1).ToList();
        //                }
        //                if (ldata.Count > 0)
        //                {

        //                    using (var cn = new MySqlConnection(_cnn2.ConnectionString))
        //                    {
        //                        cn.Open();
        //                        MySqlTransaction tr = null;

        //                        try
        //                        {
        //                            tr = cn.BeginTransaction();

        //                            var p0 = new MySql.Data.MySqlClient.MySqlParameter();
        //                            p0.ParameterName = "sucEq";
        //                            p0.Value = filtro.sucEquipo;
        //                            var sql0 = @"delete from resumen_ventas_pos where sucEquipo=@sucEq";
        //                            var comando1 = new MySqlCommand(sql0, cn, tr);
        //                            comando1.Parameters.Clear();
        //                            comando1.Parameters.Add(p0);
        //                            comando1.ExecuteNonQuery();

        //                            sql0 = @"INSERT INTO resumen_ventas_pos (autoProducto, cnt, sucEquipo) VALUES (@autoPrd, @cnt, @sucEq)";
        //                            var comando2 = new MySqlCommand(sql0, cn, tr);
        //                            var p1 = new MySql.Data.MySqlClient.MySqlParameter();
        //                            var p2 = new MySql.Data.MySqlClient.MySqlParameter();
        //                            var p3 = new MySql.Data.MySqlClient.MySqlParameter();
        //                            foreach (var dt in ldata)
        //                            {
        //                                p1.ParameterName = "autoPrd";
        //                                p1.Value = dt.autoProducto;
        //                                p2.ParameterName = "cnt";
        //                                p2.Value = dt.cnt;
        //                                p3.ParameterName = "sucEq";
        //                                p3.Value = filtro.sucEquipo;

        //                                comando2.Parameters.Clear();
        //                                comando2.Parameters.Add(p1);
        //                                comando2.Parameters.Add(p2);
        //                                comando2.Parameters.Add(p3);
        //                                comando2.ExecuteNonQuery();
        //                            }
        //                            tr.Commit();
        //                        }
        //                        catch (Exception ex1)
        //                        {
        //                            tr.Rollback();
        //                            result.Mensaje = ex1.Message;
        //                            result.Result = DtoLib.Enumerados.EnumResult.isError;
        //                        }
        //                    };

        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                result.Mensaje = e.Message;
        //                result.Result = DtoLib.Enumerados.EnumResult.isError;
        //            }

        //            return result;
        //        }

    }

}
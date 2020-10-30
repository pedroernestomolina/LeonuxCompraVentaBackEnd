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

        public DtoLib.ResultadoLista<DtoLibCompra.Producto.Lista.Resumen> Producto_GetLista(DtoLibCompra.Producto.Lista.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCompra.Producto.Lista.Resumen>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var sql_1 = "select p.auto as autoPrd, p.codigo as codigoPrd, p.nombre as descripcionPrd, p.modelo as modeloPrd, "+
                        "p.referencia as rferenciaPrd, p.categoria as categoriaPrd, p.origen as origenPrd, p.tasa as tasaIvaPrd, "+
                        "p.estatus as estatus, p.estatus_divisa as estatusDivisa,p.contenido_compras as contenidoEmpaquePrd, "+
                        "pm.nombre as empaqueCompraPrd, ed.nombre as nombreDepartamento, pg.nombre as nombreGrupo, "+
                        "pmarca.nombre as nombreMarca,etasa.nombre as tasaIvaDescripcion ";

                    var sql_2 = "from productos as p " +
                        "join empresa_departamentos as ed on p.auto_departamento=ed.auto " +
                        "join productos_grupo as pg on p.auto_grupo=pg.auto " +
                        "join productos_medida as pm on p.auto_empaque_compra=pm.auto " +
                        "join productos_marca as pmarca on p.auto_marca=pmarca.auto " +
                        "join empresa_tasas as etasa on p.auto_tasa=etasa.auto ";

                    var sql_3 =" where 1=1 ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();

                    var valor = "";
                    if (filtro.cadena != "")
                    {
                        if (filtro.MetodoBusqueda == DtoLibCompra.Producto.Enumerados.EnumMetodoBusqueda.Codigo)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql_3 += " and p.codigo like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql_3 += " and p.codigo like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.MetodoBusqueda == DtoLibCompra.Producto.Enumerados.EnumMetodoBusqueda.Nombre)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql_3 += " and p.nombre like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql_3 += " and p.nombre like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.MetodoBusqueda == DtoLibCompra.Producto.Enumerados.EnumMetodoBusqueda.Referencia)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql_3 += " and p.referencia like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql_3 += " and p.referencia like @p";
                                valor = cad + "%";
                            }
                        }
                        p1.ParameterName = "@p";
                        p1.Value = valor;
                    }
                    if (filtro.autoDepartamento != "")
                    {
                        sql_3 += " and p.auto_departamento=@autoDepartamento ";
                        p3.ParameterName = "@autoDepartamento";
                        p3.Value = filtro.autoDepartamento;
                    }
                    if (filtro.autoGrupo != "")
                    {
                        sql_3 += " and p.auto_grupo=@autoGrupo ";
                        p4.ParameterName = "@autoGrupo";
                        p4.Value = filtro.autoGrupo;
                    }
                    if (filtro.autoMarca != "")
                    {
                        sql_3 += " and p.auto_marca=@autoMarca ";
                        p5.ParameterName = "@autoMarca";
                        p5.Value = filtro.autoMarca;
                    }
                    if (filtro.autoProveedor != "")
                    {
                        sql_2 += "join productos_proveedor as pprov on p.auto=pprov.auto_producto ";
                        sql_3 +=" and pprov.auto_proveedor=@autoProveedor ";
                        p2.ParameterName = "@@autoProveedor";
                        p2.Value = filtro.autoProveedor;
                    }
                    var sql = sql_1 + sql_2 + sql_3;
                    var list= cnn.Database.SqlQuery<DtoLibCompra.Producto.Lista.Resumen>(sql, p1, p2, p3, p4, p5).ToList();
                    rt.Lista = list;
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
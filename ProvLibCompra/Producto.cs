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

        public DtoLib.ResultadoEntidad<DtoLibCompra.Producto.Data.Ficha> Producto_GetFicha(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibCompra.Producto.Data.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var entPrd = cnn.productos.Find(autoPrd);
                    if (entPrd == null) 
                    {
                        rt.Mensaje = "PRODUCTO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    var _empCompra = "";
                    var _decimales = "";
                    var entPrdMedCompra = cnn.productos_medida.Find(entPrd.auto_empaque_compra);
                    if (entPrdMedCompra != null) 
                    {
                        _empCompra=entPrdMedCompra.nombre;
                        _decimales=entPrdMedCompra.decimales;
                    }

                    var _depart = entPrd.empresa_departamentos.nombre;
                    var _codDepart = entPrd.empresa_departamentos.codigo;
                    var _grupo = entPrd.productos_grupo.nombre;
                    var _codGrupo = entPrd.productos_grupo.codigo;
                    var _marca = entPrd.productos_marca.nombre;
                    var _nombreTasaIva = entPrd.empresa_tasas.nombre;
                    var _tasaIva = entPrd.empresa_tasas.tasa;
                    var _origen = entPrd.origen;
                    var _categoria = entPrd.categoria;
                    var _estatus = DtoLibCompra.Producto.Enumerados.EnumEstatus.Activo;
                    if  (entPrd.estatus.Trim().ToUpper()=="INACTIVO") 
                         _estatus = DtoLibCompra.Producto.Enumerados.EnumEstatus.Inactivo;
                    var _admDivisa = DtoLibCompra.Producto.Enumerados.EnumAdministradorPorDivisa.Si;
                    if (entPrd.estatus_divisa.Trim().ToUpper() != "1" )
                        _admDivisa = DtoLibCompra.Producto.Enumerados.EnumAdministradorPorDivisa.No;

                    var id = new DtoLibCompra.Producto.Data.Ficha()
                    {
                        AdmPorDivisa = _admDivisa,
                        auto = entPrd.auto,
                        autoDepartamento = entPrd.auto_departamento,
                        autoGrupo = entPrd.auto_grupo,
                        autoMarca = entPrd.auto_marca,
                        autoTasa = entPrd.auto_tasa,
                        autoSubGrupo=entPrd.auto_subgrupo,
                        categoria = _categoria,
                        codigo = entPrd.codigo,
                        codigoDepartamento = _codDepart,
                        codigoGrupo = _codGrupo,
                        contenidoCompra = entPrd.contenido_compras,
                        departamento = _depart,
                        descripcion = entPrd.nombre,
                        empaqueCompra = _empCompra,
                        estatus = _estatus,
                        grupo = _grupo,
                        marca = _marca,
                        modelo = entPrd.modelo,
                        nombre = entPrd.nombre_corto,
                        nombreTasaIva = _nombreTasaIva,
                        origen = _origen,
                        referencia = entPrd.referencia,
                        tasaIva = _tasaIva,
                        fechaUltCambio = entPrd.fecha_cambio,
                        decimales = _decimales,
                        costo = entPrd.costo,
                        costoDivisa = entPrd.divisa,
                    };
                    rt.Entidad = id;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoEntidad<string> Producto_GetCodigoRefProveedor(DtoLibCompra.Producto.CodigoRefProveedor.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var entPrdPrv = cnn.productos_proveedor.FirstOrDefault(f=>f.auto_producto==filtro.autoPrd && f.auto_proveedor==filtro.autoPrv);
                    if (entPrdPrv == null)
                    {
                        rt.Entidad = "";
                        return rt;
                    }
                    rt.Entidad = entPrdPrv.codigo_producto;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoEntidad<DtoLibCompra.Producto.Utilidad.Ficha> Producto_GetUtilidadPrecio(string auto)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibCompra.Producto.Utilidad.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var entPrd = cnn.productos.Find(auto);
                    if (entPrd == null)
                    {
                        rt.Mensaje = "PRODUCTO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    var entPrdExt = cnn.productos_ext.Find(auto);
                    if (entPrdExt == null)
                    {
                        rt.Mensaje = "PRODUCTO EXT NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    var nr = new DtoLibCompra.Producto.Utilidad.Ficha()
                    {
                        auto = entPrd.auto,
                        admDivisa = entPrd.estatus_divisa.Trim().ToUpper() == "1" ? true : false,
                        contenido_1 = entPrd.contenido_1,
                        contenido_2 = entPrd.contenido_2,
                        contenido_3 = entPrd.contenido_3,
                        contenido_4 = entPrd.contenido_4,
                        contenido_5 = entPrd.contenido_pto,
                        tasaIva = entPrd.empresa_tasas.tasa,
                        utilidad_1 = entPrd.utilidad_1,
                        utilidad_2 = entPrd.utilidad_2,
                        utilidad_3 = entPrd.utilidad_3,
                        utilidad_4 = entPrd.utilidad_4,
                        utilidad_5 = entPrd.utilidad_pto,
                        precio_1 = entPrd.precio_1,
                        precio_2 = entPrd.precio_2,
                        precio_3 = entPrd.precio_3,
                        precio_4 = entPrd.precio_4,
                        precio_5 = entPrd.precio_pto,
                        //
                        contenido_may_1 = entPrdExt.contenido_may_1,
                        contenido_may_2 = entPrdExt.contenido_may_2,
                        utilidad_may_1 = entPrdExt.utilidad_may_1,
                        utilidad_may_2 = entPrdExt.utilidad_may_2,
                        precio_may_1 = entPrdExt.precio_may_1,
                        precio_may_2 = entPrdExt.precio_may_2,
                    };
                    rt.Entidad = nr;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.Resultado Producto_VerificaDepositoAsignado(DtoLibCompra.Producto.VerificarDepositoAsignado.Ficha ficha)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var entPrdDeposito = cnn.productos_deposito.FirstOrDefault(f=>f.auto_producto==ficha.autoPrd && f.auto_deposito==ficha.autoDeposito);
                    if (entPrdDeposito == null)
                    {
                        rt.Mensaje = "DEPOSITO NO ASIGNADO AL PRODUCTO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
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
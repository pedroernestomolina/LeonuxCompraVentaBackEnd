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

        public DtoLib.ResultadoLista<DtoLibCompra.Proveedor.Lista.Resumen> Proveedor_GetLista(DtoLibCompra.Proveedor.Lista.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibCompra.Proveedor.Lista.Resumen>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var sql = "select p.auto, p.codigo, p.razon_social as nombreRazonSocial, p.ci_rif as ciRif, " +
                        "p.dir_fiscal as dirFiscal, p.telefono, p.contacto as nombreContacto, p.estatus , " +
                        "g.nombre as nombreGrupo, e.nombre as nombreEstado "+
                        "FROM proveedores as p "+
                        "join proveedores_grupo as g on p.auto_grupo=g.auto "+
                        "join sistema_estados as e on p.auto_estado=e.auto "+
                        "where 1=1 ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();

                    var valor = "";
                    if (filtro.cadena != "")
                    {
                        if (filtro.MetodoBusqueda == DtoLibCompra.Proveedor.Enumerados.EnumMetodoBusqueda.Codigo)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql += " and p.codigo like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql += " and p.codigo like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.MetodoBusqueda == DtoLibCompra.Proveedor.Enumerados.EnumMetodoBusqueda.Nombre )
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql += " and p.razon_social like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql += " and p.razon_social like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.MetodoBusqueda == DtoLibCompra.Proveedor.Enumerados.EnumMetodoBusqueda.CiRif )
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql += " and p.ci_rif like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql += " and p.ci_rif like @p";
                                valor = cad + "%";
                            }
                        }
                        p1.ParameterName = "@p";
                        p1.Value = valor;
                    }

                    if (filtro.autoGrupo != "")
                    {
                        sql += " and p.auto_grupo=@autoGrupo ";
                        p2.ParameterName = "@autoGrupo";
                        p2.Value = filtro.autoGrupo;
                    }
                    if (filtro.autoEstado != "")
                    {
                        sql += " and p.auto_estado=@autoEstado ";
                        p3.ParameterName = "@autoEstado";
                        p3.Value = filtro.autoEstado;
                    }
                    if (filtro.estatus != DtoLibCompra.Proveedor.Enumerados.EnumEstatus.SnDefinir)
                    {
                        var f = "Activo";
                        if (filtro.estatus == DtoLibCompra.Proveedor.Enumerados.EnumEstatus.Inactivo)
                            f = "Inactivo";
                        sql += " and p.estatus=@estatus ";
                        p4.ParameterName = "@estatus";
                        p4.Value = f;
                    }
                    var list = cnn.Database.SqlQuery<DtoLibCompra.Proveedor.Lista.Resumen>(sql, p1, p2, p3, p4).ToList();
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

        public DtoLib.ResultadoLista<DtoLibCompra.Proveedor.Data.Ficha> Proveedor_GetFicha(string autoPrv)
        {
            throw new NotImplementedException();
        }

    }

}
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


        public DtoLib.ResultadoEntidad<DtoLibCompra.Deposito.Data.Ficha> Deposito_GetFicha(string autoDeposito)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Deposito.Data.Ficha>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var ent = cnn.empresa_depositos.Find(autoDeposito);

                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] DEPOSITO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var _autoSuc = "";
                    var _codSuc = "";
                    var _nomSuc = "";
                    var entSuc = cnn.empresa_sucursal.FirstOrDefault(f => f.codigo == ent.codigo_sucursal);
                    if (entSuc != null)
                    {
                        _autoSuc = entSuc.auto;
                        _codSuc = entSuc.codigo;
                        _nomSuc = entSuc.nombre;
                    };

                    var nr = new DtoLibCompra.Deposito.Data.Ficha()
                    {
                        auto = ent.auto,
                        codigo = ent.codigo,
                        nombre = ent.nombre,
                        autoSucursal = _autoSuc,
                        codigoSucursal = _codSuc,
                        nombreSucursal = _nomSuc,
                    };
                    result.Entidad = nr;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoLista<DtoLibCompra.Deposito.Lista.Resumen> Deposito_GetLista(DtoLibCompra.Deposito.Lista.Filtro filtro)
        {

            var result = new DtoLib.ResultadoLista<DtoLibCompra.Deposito.Lista.Resumen>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = " select auto as id, codigo, nombre, codigo_sucursal as codigoSuc ";
                    var sql_2 = " from empresa_depositos ";
                    var sql_3 = " where 1=1 ";
                    var sql_4 = "";

                    if (filtro.PorCodigoSuc != "")
                    {
                        sql_3 += " and codigo_sucursal=@p1";
                        p1.ParameterName = "@p1";
                        p1.Value = filtro.PorCodigoSuc;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibCompra.Deposito.Lista.Resumen>(sql, p1).ToList();
                    result.Lista = list;
                }
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
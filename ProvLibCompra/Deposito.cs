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

        public DtoLib.ResultadoLista<DtoLibCompra.Deposito.Lista.Resumen> Deposito_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibCompra.Deposito.Lista.Resumen>();

            try
            {
                using (var cnn = new compraEntities(_cnCompra.ConnectionString))
                {
                    var q = cnn.empresa_depositos.ToList();

                    var list = new List<DtoLibCompra.Deposito.Lista.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var r = new DtoLibCompra.Deposito.Lista.Resumen()
                                {
                                    auto = s.auto,
                                    codigo = s.codigo,
                                    nombre = s.nombre,
                                };
                                return r;
                            }).ToList();
                        }
                    }
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

    }

}
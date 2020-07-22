using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibInventario.Deposito.Resumen> Deposito_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Deposito.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q = cnn.empresa_depositos.ToList();

                    var list = new List<DtoLibInventario.Deposito.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var r = new DtoLibInventario.Deposito.Resumen()
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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Deposito.Ficha> Deposito_GetFicha(string autoDep)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Deposito.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.empresa_depositos.Find(autoDep);

                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] DEPOSITO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var _autoSuc="";
                    var _codSuc="";
                    var _nomSuc="";
                    var entSuc= cnn.empresa_sucursal.FirstOrDefault(f=>f.codigo==ent.codigo_sucursal);
                    if (entSuc!=null)
                    {
                        _autoSuc=entSuc.auto;
                        _codSuc=entSuc.codigo;
                        _nomSuc=entSuc.nombre;
                    };

                    var nr = new DtoLibInventario.Deposito.Ficha()
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
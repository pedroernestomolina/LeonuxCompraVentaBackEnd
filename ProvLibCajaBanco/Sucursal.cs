using LibEntityCajaBanco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibCajaBanco
{

    public partial class Provider : ILibCajaBanco.IProvider 
    {

        public DtoLib.ResultadoLista<DtoLibCajaBanco.Sucursal.Resumen> Sucursal_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibCajaBanco.Sucursal.Resumen>();

            try
            {
                using (var cnn = new cajaBancoEntities(_cnCajBanco.ConnectionString))
                {
                    var q = cnn.empresa_sucursal.ToList();

                    var list = new List<DtoLibCajaBanco.Sucursal.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var r = new DtoLibCajaBanco.Sucursal.Resumen()
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

    }

}
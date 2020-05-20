using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibVenta
{

    public partial class Provider: ILibVentas.IProvider 
    {

        public DtoLib.ResultadoLista<DtoLibVenta.Cobrador.Resumen> CobradorLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibVenta.Cobrador.Resumen>();

            try
            {
                using (var cnn = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var list = new List<DtoLibVenta.Cobrador.Resumen>();
                    var q = cnn.empresa_cobradores.ToList();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            result.Lista = q.Select(s =>
                            {
                                var r = new DtoLibVenta.Cobrador.Resumen()
                                {
                                    Auto = s.auto,
                                    Codigo = s.codigo,
                                    Nombre = s.nombre,
                                };
                                return r;
                            }).ToList();
                        }
                        else
                        {
                            result.Lista = list;
                        }
                    }
                    else
                    {
                        result.Lista = list;
                    }
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
using LibEntitySistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibSistema
{

    public partial class Provider : ILibSistema.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibSistema.Funciones.Resumen> Funcion_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibSistema.Funciones.Resumen >();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var q = cnn.sistema_funciones.ToList();

                    var list = new List<DtoLibSistema.Funciones.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var r = new DtoLibSistema.Funciones.Resumen()
                                {
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

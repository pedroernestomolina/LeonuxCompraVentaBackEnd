using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{

    public partial class Provider : IPosOffLine.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibPosOffLine.MedioCobro.Ficha> MedioCobro_Lista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibPosOffLine.MedioCobro.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var q = cnn.MedioCobro.ToList();
                    var list = new List<DtoLibPosOffLine.MedioCobro.Ficha>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            result.Lista = q.Select(s =>
                            {
                                var r = new DtoLibPosOffLine.MedioCobro.Ficha()
                                {
                                    Auto = s.auto,
                                    Codigo = s.codigo,
                                    Nombre = s.descripcion,
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